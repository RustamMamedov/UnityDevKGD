using System.Collections.Generic;
using UnityEngine;
using Events;
using UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Sirenix.OdinInspector;

namespace Game {

    public class Save : MonoBehaviour {

        [Serializable]
        public class SaveData {

            public string data;
            public string score;
        }

        [Serializable]
        private class SavedDataWrapper {

            public List<SaveData> savedDatas;
        }

        private enum SaveType {

            PlayerPrefs,
            File
        }

        [SerializeField]
        private EventListener _carCollisionEventListener;

        [SerializeField]
        private ScriptableIntValue _currentScore;

        [SerializeField]
        [InfoBox("$message", nameof(SavingToPlayerPrefsOrToFile))]
        private SaveType _saveType;
        private string message;
        public string InfoBoxMessage {
            get {
                return (Path.Combine(Application.persistentDataPath,"data.txt"));
            }
        }

        private List<SaveData> _saveDatas;
        public List<SaveData> SavedDatas => _saveDatas;

        private const string RECORDS_KEY = "records";
        private static string _filePath;
        

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

        private void OnEnable() {

            _carCollisionEventListener.OnEventHappened += OnCarCollision;
        }

        private void OnDisable() {

            _carCollisionEventListener.OnEventHappened -= OnCarCollision;
        }

        private void OnCarCollision() {

            var newRecord = new SaveData {
                data = DateTime.Now.ToString("MM/dd/yyyy HH:mm"),
                score = _currentScore.value.ToString()
            };

            if (_saveDatas.Count != 10) {

                _saveDatas.Add(newRecord);
            }
            else {

                for (int i = 0; i < _saveDatas.Count - 1; i++) {

                    if (Int32.Parse(newRecord.score) >= Int32.Parse(_saveDatas[i].score)) {

                        var a = _saveDatas[i].score;
                        var b = _saveDatas[i].data;
                        _saveDatas[i].score = newRecord.score;
                        _saveDatas[i].data = newRecord.data;
                        newRecord.score = a;
                        newRecord.data = b;

                    }
                }
            }

            if (_saveType == SaveType.PlayerPrefs) {

                SaveToPlayerPrefs();
            }
            else {
                SaveToFile();
            }
            UIManager.Instansce.ShowLeaderboardScreen();
        }

        private void LoadFromPlayerPrefs() {

            if (!PlayerPrefs.HasKey(RECORDS_KEY)) {

                return;
            }

            var wrapper = JsonUtility.FromJson<SavedDataWrapper>(PlayerPrefs.GetString(RECORDS_KEY));
            _saveDatas = wrapper.savedDatas;

        }
        private void SaveToPlayerPrefs() {
            var wrapper = GetWrapper();
            var json = JsonUtility.ToJson(wrapper);
            PlayerPrefs.SetString(RECORDS_KEY, json);
        }


        private SavedDataWrapper GetWrapper() {

            var wrapper = new SavedDataWrapper {
                savedDatas = _saveDatas
            };
            return wrapper;
        }

        private void LoadFromFile() {
            if (!File.Exists(_filePath)) {

                return;
            }

            var binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = File.Open(_filePath, FileMode.OpenOrCreate)) {
                var wrapper = (SavedDataWrapper)binaryFormatter.Deserialize(fileStream);
                _saveDatas = wrapper.savedDatas;
            }
            Debug.Log(_saveDatas.Count);
        }

        private void SaveToFile() {

            var wrapper = GetWrapper();

            var binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = File.Open(_filePath, FileMode.OpenOrCreate)) {
                binaryFormatter.Serialize(fileStream, wrapper);
                _saveDatas = wrapper.savedDatas;
            }
        }

        private bool SavingToPlayerPrefsOrToFile() {
            if (_saveType == SaveType.PlayerPrefs) {
                message = "PlayerPrefs";
               
            }
            else {
                message = InfoBoxMessage;
            }
            return true;
        }
    }
}

