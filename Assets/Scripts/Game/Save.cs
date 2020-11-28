using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UI;
using Sirenix.OdinInspector;

namespace Game {

    [Serializable]
    public class SaveData {

        public string date;
        public string score;
    }

    [Serializable]
    public class SavedDataWrapper {

        public List<SaveData> saveDatas;
    }

    public class Save : MonoBehaviour {

        [InfoBox("PlayerPrefs", "isSaveTypePlayerPrefs")]
        [InfoBox("C:/Users/nikit/AppData/LocalLow/DefaultCompany/UnityDev2020/data.txt", "isSaveTypeFile")]
        private enum SaveType {
            PlayerPrefs,
            File
        }

        [SerializeField]
        private EventListener _carCollsionEventListener;

        [SerializeField]
        private ScriptableIntValue _currentScore;

        private static List<SaveData> _savedDatas;
        private static List<SaveData> _savedDatasPrevious;

        public static List<SaveData> SaveDatas => _savedDatas;

        private const string RECORDS_KEY = "records";
     
        private string _filePath;
        private int _minScore = 0;

        private void Awake() {
            _savedDatas = new List<SaveData>();
            _filePath = Path.Combine(Application.persistentDataPath, "data.txt");

            if (_saveType == SaveType.PlayerPrefs) {
                LoadFromPlayerPrefs();
            }
            else {
                LoadFromFile();
            }
        }

        private void OnEnable() {
            _carCollsionEventListener.OnEventHappened += OnCarCollision;
        }
        private void OnDisable() {
            _carCollsionEventListener.OnEventHappened += OnCarCollision;
        }

        private void OnCarCollision() {

            var newRecord = new SaveData {
                date = DateTime.Now.ToString("MM/dd/yyyy HH:mm"),
                score = _currentScore.value.ToString()
            };

            _savedDatas.Add(newRecord);

            if (_savedDatas.Count > 10) {
                DeleteMinElement();
            }

            Debug.Log($"new record: {newRecord.date}  {newRecord.score}");

            for (int i = 0; i < _savedDatas.Count - 1; i++) {
                for (int j = 0; j < _savedDatas.Count - 1; j++) {
                    if (int.Parse(_savedDatas[i].score) < int.Parse(_savedDatas[i + 1].score)) {
                        var temp = _savedDatas[i + 1];
                        _savedDatas[i + 1] = _savedDatas[i];
                        _savedDatas[i] = temp;
                    }
                }
            }
            // не получилось сделать сортировку сохраненных данных


            if (_saveType == SaveType.PlayerPrefs) {
                SaveToPlayerPrefs();
            }
            else {
                SaveToFile();
            }


            UIManager.Instance.ShowLeaderboardsScreen();

        }

        private void DeleteMinElement() {
            var values = new List<int>();
            foreach (var obj in _savedDatas) {
                values.Add(int.Parse(obj.score));
            }
            values.Sort();
            values.Reverse();
            var minElement = values[values.Count - 1];

            for (int i = _savedDatas.Count - 1; i > -1; i--) {
                if (int.Parse(_savedDatas[i].score) == minElement) {
                    _savedDatas.RemoveAt(i);
                    break;
                }
            }
        }

        private bool isSaveTypePlayerPrefs() {
            return _saveType == SaveType.PlayerPrefs;
        }

        private bool isSaveTypeFile() {
            return _saveType == SaveType.File;
        }

        [SerializeField]
        private SaveType _saveType;

        private SavedDataWrapper GetDataWrapper() {
            var wrapper = new SavedDataWrapper {
                saveDatas = _savedDatas
            };
            return wrapper;
        }

        private void LoadFromPlayerPrefs() {
            if (!PlayerPrefs.HasKey(RECORDS_KEY)) {
                return;
            }

            var wrapper = JsonUtility.FromJson<SavedDataWrapper>(PlayerPrefs.GetString(RECORDS_KEY));
            _savedDatas = wrapper.saveDatas;
        }

        private void SaveToPlayerPrefs() {
            var wrapper = GetDataWrapper();

            var json = JsonUtility.ToJson(wrapper);
            PlayerPrefs.SetString(RECORDS_KEY, json);          
        }

        private void LoadFromFile() {

            if (!File.Exists(_filePath)) {
                return;
            }

            var binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = File.Open(_filePath, FileMode.Open)) {

                var wrapper = (SavedDataWrapper)binaryFormatter.Deserialize(fileStream);
                _savedDatas = wrapper.saveDatas;
            }       

        }

        private void SaveToFile() {

            var wrapper = GetDataWrapper();
            var binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = File.Open(_filePath, FileMode.OpenOrCreate)) {
                binaryFormatter.Serialize(fileStream, wrapper);
            }
        }


    }

}

