using Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Values;

namespace Game {
    
    public class SaveManager : MonoBehaviour {

        // Nested types.

        private enum SaveType {
            None = 0,
            PlayerPrefs,
            File
        }

        [Serializable]
        public class Record {
            public string date;
            public string score;
        }

        [Serializable]
        private class RecordsWrapper {
            public List<Record> records;
        }


        // Constants.

        private const string RECORDS_KEY = "records";


        // Fields.

        [SerializeField]
        private SaveType _saveType;

        [SerializeField]
        private EventListener _carCollisionEventListener;

        [SerializeField]
        private ScriptableIntValue _currentScoreValue;

        private List<Record> _records;
        private string _saveFilePath;


        // Life cycle.

        private void Awake() {
            _records = new List<Record>();
            _saveFilePath = Path.Combine(Application.persistentDataPath, "records.save");
            LoadData();
        }

        private void OnEnable() {
            _carCollisionEventListener.OnEventHappened += OnCarCollision;
        }

        private void OnDisable() {
            _carCollisionEventListener.OnEventHappened -= OnCarCollision;
        }


        // Properties.

        public List<Record> SavedDatas => _records;


        // Event handling.

        private void OnCarCollision() {
            var newRecord = new Record {
                date = DateTime.Now.ToString("DD.MM.yyyy HH:mm"),
                score = _currentScoreValue.value.ToString()
            };
            _records.Add(newRecord);
            SaveData();
        }


        // Data loading/saving.

        private void LoadData() {
            if (_saveType == SaveType.PlayerPrefs) {
                LoadDataFromPlayerPrefs();
            } else if (_saveType == SaveType.File) {
                LoadDataFromFile();
            }
        }

        private void SaveData() {
            if (_saveType == SaveType.PlayerPrefs) {
                SaveDataToPlayerPrefs();
            } else if (_saveType == SaveType.File) {
                SaveDataToFile();
            }
        }

        private void LoadDataFromPlayerPrefs() {
            if (!PlayerPrefs.HasKey(RECORDS_KEY)) {
                return;
            }
            var json = PlayerPrefs.GetString(RECORDS_KEY);
            var wrapper = JsonUtility.FromJson<RecordsWrapper>(json);
            _records = wrapper.records;
        }

        private void SaveDataToPlayerPrefs() {
            var wrapper = new RecordsWrapper {
                records = _records
            };
            var json = JsonUtility.ToJson(wrapper);
            PlayerPrefs.SetString(RECORDS_KEY, json);
        }

        private void LoadDataFromFile() {
            if (!File.Exists(_saveFilePath)) {
                return;
            }
            var binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = File.Open(_saveFilePath, FileMode.Open)) {
                var wrapper = (RecordsWrapper) binaryFormatter.Deserialize(fileStream);
                _records = wrapper.records;
            }
        }

        private void SaveDataToFile() {
            var wrapper = new RecordsWrapper {
                records = _records
            };
            var binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = File.Open(_saveFilePath, FileMode.OpenOrCreate)) {
                binaryFormatter.Serialize(fileStream, wrapper);
            }
        }


    }

}
