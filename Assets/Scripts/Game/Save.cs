using System;
using System.Collections.Generic;
using Events;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Game {

    public class Save : MonoBehaviour {

        #region Data
        [Serializable]
        public class SaveData {

            public string date;
            public string score;
        }

        [Serializable]
        private class SavedDataWrapper {

            public List<SaveData> saveDatas;
        }

        [InfoBox("@Info()")]
        private enum SaveType {

            PlayerPrefs,
            File,
        }

        [SerializeField]
        private EventListener _carCollisionEventListener;

        [SerializeField]
        private EventDispatcher _dataSaved;

        [SerializeField]
        private ScriptableIntValue _currentScore;

        [SerializeField]
        private SaveType _saveType;

        private static List<SaveData> _saveDatas;
        public static List<SaveData> SavedDatas => _saveDatas;
        public static int position;

        private const string RECORDS_KEY = "records";
        private string _filePath;

        private string Info() {
            if (_saveType == SaveType.File)
                return _filePath;
            else
                return "PlayerPrefs";
        }

        #endregion Data

        private void Awake() {
            _saveDatas = new List<SaveData>();
            _filePath = Path.Combine(Application.persistentDataPath, "data.txt");
            if (_saveType == SaveType.PlayerPrefs) {
                LoadFromPlayerPrefs();
            }
            else {
                LoadFromFile();
            }
        }

        #region Enable/Disable
        private void OnEnable() {
            _carCollisionEventListener.OnEventHappened += OnCarCollision;
        }

        private void OnDisable() {
            _carCollisionEventListener.OnEventHappened -= OnCarCollision;
        }
        #endregion Enable/Disable

        private void OnCarCollision() {
            var newRecord = new SaveData {
                date = DateTime.Now.ToString("MM/dd/yyyy HH:mm"),
                score = _currentScore.value.ToString()
            };
            _saveDatas.Add(newRecord);

            position=SortandRemoveRecords(newRecord);
            

            if (_saveType == SaveType.PlayerPrefs) {
                SaveToPlayerPrefs();
            }
            else {
                SaveToFile();
            }

            _dataSaved.Dispatch();
        }

        private SavedDataWrapper GetWrapper() {
            var wrapper = new SavedDataWrapper {
                saveDatas = _saveDatas
            };
            return wrapper;
        }

        private int SortandRemoveRecords(SaveData current) {
            for(int i=0;i<_saveDatas.Count-1;i++)
                for (int j = i+1; j < _saveDatas.Count; j++) {
                    if(int.Parse(_saveDatas[i].score)< int.Parse(_saveDatas[j].score)) {//int32 почему-то не робит, вроде в System но не робит поэтому просто int.Parse
                        var tmp = _saveDatas[i];
                        _saveDatas[i] = _saveDatas[j];
                        _saveDatas[j] = tmp;
                    }
                }
            while (_saveDatas.Count > 10)
                _saveDatas.RemoveAt(_saveDatas.Count - 1);
            for (int i = 0; i < _saveDatas.Count - 1; i++)
                if (_saveDatas[i] == current) {
                    return i;
                }
            return -1;
        }

        #region PlayerPrefs
        private void LoadFromPlayerPrefs() {
            if (!PlayerPrefs.HasKey(RECORDS_KEY)) {
                return;
            }

            var wrapper = JsonUtility.FromJson<SavedDataWrapper>(PlayerPrefs.GetString(RECORDS_KEY));
            _saveDatas = wrapper.saveDatas;
        }

        private void SaveToPlayerPrefs() {
            var wrapper = GetWrapper();
            var json = JsonUtility.ToJson(wrapper);
            PlayerPrefs.SetString(RECORDS_KEY, json);
        }
        #endregion PlayerPrefs

        #region File
        private void LoadFromFile() {
            if (!File.Exists(_filePath)) {
                return;
            }

            var binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = File.Open(_filePath, FileMode.Open)) {
                var wrapper = (SavedDataWrapper)binaryFormatter.Deserialize(fileStream);
                _saveDatas = wrapper.saveDatas;
            }
            Debug.Log(_saveDatas.Count);
        }

        private void SaveToFile() {
            var wrapper = GetWrapper();
            var binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = File.Open(_filePath, FileMode.OpenOrCreate)) {
                binaryFormatter.Serialize(fileStream, wrapper);
            }
        }
        #endregion File
    }
}