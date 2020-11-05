using Events;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
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
        public class Record : ISerializable, IComparable<Record> {

            // Constants.

            private const string _dateTimeKey = "dateTime";
            private const string _scoreKey = "score";


            // Fields.

            private readonly DateTime _dateTime;
            private readonly int _score;


            // Constructor.

            public Record() : this(
                dateTime: default,
                score: 0
            ) {}

            public Record(DateTime dateTime, int score) {
                _dateTime = dateTime;
                _score = score;
            }


            // Deserialization/Serialization.

            public Record(SerializationInfo info, StreamingContext context) {
                try {
                    _dateTime = (DateTime) info.GetValue(_dateTimeKey, typeof(DateTime));
                    _score = (int) info.GetValue(_scoreKey, typeof(int));
                } catch (Exception exception) when (exception is InvalidCastException || exception is SerializationException) {
                    // Make record invalid if deserialization fails.
                    _dateTime = new DateTime();
                    _score = -1;
                }
            }

            public void GetObjectData(SerializationInfo info, StreamingContext context) {
                info.AddValue(_dateTimeKey, _dateTime);
                info.AddValue(_scoreKey, _score);
            }


            // Properties.

            public DateTime DateTime => _dateTime;
            public int Score => _score;
            public bool IsValid => _score >= 0;


            // Comparison.

            // More score is better. Otherwise less time is better.
            public int CompareTo(Record other) {
                if (_score < other._score) {
                    return -1;
                }
                if (_score > other._score) {
                    return 1;
                }
                if (_dateTime > other._dateTime) {
                    return -1;
                }
                if (_dateTime < other._dateTime) {
                    return 1;
                }
                return 0;
            }


        }

        [Serializable]
        private class RecordsWrapper {

            public List<Record> records;


        }


        // Constants.

        private const string RECORDS_KEY = "records";


        // Fields.

        [SerializeField]
        private int _recordsLimit;

        [SerializeField]
        private SaveType _saveType;

        [SerializeField]
        private EventListener _carCollisionEventListener;

        [SerializeField]
        private ScriptableIntValue _currentScoreValue;

        // The records list is maintained:
        // - to be sorted in descending order.
        // - to contain no more than {_recordsLimit} records.
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

        public Record[] Records => _records.ToArray();


        // Event handling.

        private void OnCarCollision() {
            var newRecord = new Record(
                dateTime: DateTime.Now,
                score: _currentScoreValue.value
            );
            AddRecord(newRecord);
            SaveData();
        }


        // Records processing.

        // Add new record and maintain the records list.
        private void AddRecord(Record newRecord) {
            if (!newRecord.IsValid) {
                return;
            }
            int index = _records.FindIndex(otherRecord => newRecord.CompareTo(otherRecord) > 0);
            if (index == -1) {
                index = _records.Count;
            }
            _records.Insert(index, newRecord);
            if (_records.Count > _recordsLimit) {
                _records.RemoveAt(_records.Count - 1);
            }
        }

        // Reorganize records to maintain the records list.
        private void ProcessRecords() {
            _records.Sort((first, second) => -first.CompareTo(second));
            while (_records.Count > 0) {
                var lastIndex = _records.Count - 1;
                if (_records[lastIndex].IsValid && _records.Count <= _recordsLimit) {
                    break;
                }
                _records.RemoveAt(lastIndex);
            }
        }


        // Data loading/saving.

        private void LoadData() {
            if (_saveType == SaveType.PlayerPrefs) {
                LoadDataFromPlayerPrefs();
            } else if (_saveType == SaveType.File) {
                LoadDataFromFile();
            }
            ProcessRecords();
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
