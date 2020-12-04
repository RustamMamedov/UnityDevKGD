using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization.Formatters.Binary;
using Events;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game {
    
    public class Save : MonoBehaviour {

#region Inner Definitions

        [Serializable]
        public class Record : IComparable<Record>{
            
            // Left public to allow JSON serialize these fields
            public string date;
            public string score;

            private DateTime _dateTime;

            public int Score => Int32.Parse(score);
            public DateTime Date => DateTime.ParseExact(date, "MM/dd/yyyy HH:mm", CultureInfo.InvariantCulture);
            
            public int CompareTo(Record other) {
                if (other.Score != Score) {
                    return Score > other.Score ? -1 : 1;
                }

                try {
                    return Date.CompareTo(other.Date);
                } catch (Exception) {
                    return date.CompareTo(other.date);
                }
            }

            public bool IsValid() {
                return !String.IsNullOrEmpty(date) && !String.IsNullOrEmpty(score);
            }
        }

        [Serializable]
        private class SavedDataWrapper {

            public List<Record> saveDatas;
        }

        private enum SaveType {
            
            PlayerPrefs,
            File
        }
#endregion

#region Fields

        [SerializeField]
        private EventListener _carCollisionEventListener;

        [SerializeField] 
        private EventDispatcher _dataSavedEventDispatcher;
        
        [SerializeField]
        private ScriptableIntValue _currentScore;

        [InfoBox("@GetSaveInfo()", InfoMessageType.Info)]
        [SerializeField]
        private SaveType _saveType;

        [Range(0, 20)]
        [SerializeField] 
        private int _maxRecordsCountToSave;

        private static List<Record> _saveDatas;
        
        public static List<Record> SaveDatas => _saveDatas;

        private static int _indexOfCurrentRideInLeaderboard;

        public static int IndexOfCurrentRideInLeaderboard => _indexOfCurrentRideInLeaderboard;

        private const string RECORDS_KEY = "records";

        private string _filePath;
        
#endregion

#region LifeCycle

        private void Awake() {
            _saveDatas = new List<Record>();
            _filePath = Path.Combine(Application.persistentDataPath, "records.save");

            if (_saveType == SaveType.PlayerPrefs) {
                LoadFromPlayerPrefs();
            } else {
                LoadFromFile();
            }

            CheckRecords();
        }
        
        private void OnEnable() {
            _carCollisionEventListener.OnEventHappened += OnCarCollision;
        }

        private void OnDisable() {
            _carCollisionEventListener.OnEventHappened -= OnCarCollision;
        }
#endregion

#region OnEvent handlers

        private void OnCarCollision() {
            var newRecord = new Record {
                date = DateTime.Now.ToString("MM/dd/yyyy HH:mm"),
                score = _currentScore.value.ToString()
            };
            
            AddRecord(newRecord);
            
            if (_saveType == SaveType.PlayerPrefs) {
                SaveToPlayerPrefs();
            } else {
                SaveToFile();
            }
            
            _dataSavedEventDispatcher.Dispatch();
        }
#endregion

#region Additive Methods
        
        private void AddRecord(Record record) {
            if (_saveDatas.Count == 0) {
                _indexOfCurrentRideInLeaderboard = 1;
                _saveDatas.Add(record);
                return;
            }
            
            // Index of place where we should place our record depending on order
            int index = -1;
            
            index = _saveDatas.FindIndex(other => record.CompareTo(other) < 0);
            if (index == -1) {
                index = _saveDatas.Count;
            }
            
            _saveDatas.Insert(index, record);
        
            if (_saveDatas.Count > _maxRecordsCountToSave) {
                _saveDatas.RemoveAt(_saveDatas.Count - 1);
            }

            _indexOfCurrentRideInLeaderboard = index + 1;
        }
        
        private void CheckRecords() {
            // If data was corrupted delete it from list
            for (int i = _saveDatas.Count - 1; i > -1; --i) {
                if (!_saveDatas[i].IsValid()) {
                    _saveDatas.RemoveAt(i);
                }
            }
            
            // Sort records in descending order
            _saveDatas.Sort((first, second) => first.CompareTo(second));
            
            // Removing rear elements if list size is more than available
            while (_saveDatas.Count > _maxRecordsCountToSave) {
                _saveDatas.RemoveAt(_saveDatas.Count - 1);
            }
        }

        private SavedDataWrapper GetWrapper() {
            var wrapper = new SavedDataWrapper {
                saveDatas = _saveDatas
            };

            return wrapper;
        }
#endregion

#region Save/Load Methods

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

        private void LoadFromFile() {
            if (!File.Exists(_filePath)) {
                return;
            }
            
            var binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = File.Open(_filePath, FileMode.Open)) {
                var wrapper = (SavedDataWrapper) binaryFormatter.Deserialize(fileStream);
                _saveDatas = wrapper.saveDatas;
            }
        }

        private void SaveToFile() {
            var wrapper = GetWrapper();
            
            var binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = File.Open(_filePath, FileMode.OpenOrCreate)) {
                binaryFormatter.Serialize(fileStream, wrapper);
            }
        }
#endregion

#region EditorMethods

        private string GetSaveInfo() {
            if (_saveType == SaveType.File) {
                return Path.Combine(Application.persistentDataPath, "records.save");
            }

            return "PlayerPrefs";
        }
#endregion
    }

}
