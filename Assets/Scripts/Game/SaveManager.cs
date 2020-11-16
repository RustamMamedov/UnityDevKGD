using Events;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Utilities;
using Values;

namespace Game {
    
    public class SaveManager : GameSingletonBase<SaveManager> {

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

        public const int invalidIndex = -1;
        private const string _recordsKey = "records";


        // Fields.

        [SerializeField]
        private int _recordsLimit;

        [InfoBox("$" + nameof(GetSaveDescription))]
        [SerializeField]
        private SaveType _saveType;

        private string GetSaveDescription() {
            if (_saveType == SaveType.File) {
                return $"File {GetSaveFilePath()} is used for saving.";
            } else if (_saveType == SaveType.PlayerPrefs) {
                return "PlayerPrefs are used for saving.";
            } else {
                return "No saving!";
            }
        }

        [SerializeField]
        private EventListener _carCollisionEventListener;

        [SerializeField]
        private EventDispatcher _progressSavedEventDispatcher;

        [SerializeField]
        private ScriptableIntValue _currentScoreValue;

        // The records list is maintained:
        // - to be sorted in descending order.
        // - to contain no more than {_recordsLimit} records.
        private List<Record> _records;

        private int _newRecordIndex;

        private string _saveFilePath;


        // Life cycle.

        protected override void Awake() {
            base.Awake();
            _records = new List<Record>();
            _newRecordIndex = invalidIndex;
            _saveFilePath = GetSaveFilePath();
            LoadAndSetData();
        }

        private void OnEnable() {
            _carCollisionEventListener.OnEventHappened += OnCarCollision;
        }

        private void OnDisable() {
            _carCollisionEventListener.OnEventHappened -= OnCarCollision;
        }


        // Properties.

        public Record[] Records => _records.ToArray();

        public int NewRecordIndex => _newRecordIndex;


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
            
            _newRecordIndex = invalidIndex;
            if (newRecord == null || !newRecord.IsValid) {
                return;
            }

            // Find insertion position.
            int index = _records.FindIndex(otherRecord => newRecord.CompareTo(otherRecord) > 0);
            if (index == -1) {
                index = _records.Count;
            }

            // Insert record.
            if (index >= _recordsLimit) {
                return;
            }
            _records.Insert(index, newRecord);
            _newRecordIndex = index;
            if (_records.Count > _recordsLimit) {
                _records.RemoveAt(_records.Count - 1);
            }

        }

        // Set and process records list.
        private void SetRecords(IEnumerable<Record> newRecords) {

            var records = new List<Record>();
            if (newRecords != null) {

                // Transfer valid records to new list.
                foreach (var record in newRecords) {
                    if (record == null || !record.IsValid) {
                        continue;
                    }
                    records.Add(record);
                }

                // Sort records, remove invalid ones.
                records.Sort((first, second) => -first.CompareTo(second));
                while (records.Count > _recordsLimit) {
                    records.RemoveAt(records.Count - 1);
                }

            }
          
            // Set fields.
            _records = records;
            _newRecordIndex = invalidIndex;

        }


        // Data loading/saving.

        private string GetSaveFilePath() => Path.Combine(Application.persistentDataPath, "records.save");

        private void LoadAndSetData() {
            List<Record> loadedRecords = null;
            if (_saveType == SaveType.PlayerPrefs) {
                LoadDataFromPlayerPrefs(out loadedRecords);
            } else if (_saveType == SaveType.File) {
                LoadDataFromFile(out loadedRecords);
            }
            SetRecords(loadedRecords);
        }

        private void SaveData() {
            if (_saveType == SaveType.PlayerPrefs) {
                SaveDataToPlayerPrefs();
            } else if (_saveType == SaveType.File) {
                SaveDataToFile();
            }
            _progressSavedEventDispatcher.Dispatch();
        }

        private void LoadDataFromPlayerPrefs(out List<Record> loadedRecords) {
            if (!PlayerPrefs.HasKey(_recordsKey)) {
                loadedRecords = null;
                return;
            }
            var json = PlayerPrefs.GetString(_recordsKey);
            var wrapper = JsonUtility.FromJson<RecordsWrapper>(json);
            loadedRecords = wrapper.records;
        }

        private void SaveDataToPlayerPrefs() {
            var wrapper = new RecordsWrapper {
                records = _records
            };
            var json = JsonUtility.ToJson(wrapper);
            PlayerPrefs.SetString(_recordsKey, json);
        }

        private void LoadDataFromFile(out List<Record> loadedRecords) {
            if (!File.Exists(_saveFilePath)) {
                loadedRecords = null;
                return;
            }
            var binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = File.Open(_saveFilePath, FileMode.Open)) {
                var wrapper = (RecordsWrapper) binaryFormatter.Deserialize(fileStream);
                loadedRecords = wrapper.records;
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
