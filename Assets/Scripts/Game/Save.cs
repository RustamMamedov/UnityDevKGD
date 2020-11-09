using System.Collections.Generic;
using UnityEngine;
using System;
using Events;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Game {

    public class Save : MonoBehaviour {

        [Serializable]
        public class SaveData {

            public string date;
            public string score;
        }

        [Serializable]
        private class SavedDataWrapper {

            public List<SaveData> saveDatas;
        }

        private enum SaveType {
            PlayerPrefs,
            File
        }

        [SerializeField]
        private EventListener _carCollisionEventListener;

        [SerializeField]
        private EventDispatcher _gameSavedEventDispatcher;

        [SerializeField]
        private ScriptableIntValue _currentScore;

        [SerializeField]
        private SaveType _saveType;

        [SerializeField]
        private int _numberRecordsInTable;

        private static List<SaveData> _saveDatas;
        public static List<SaveData> SavedDatas => _saveDatas;

        private const string RECORDS_KEY = "records";
        private string _filePath;

        private void Awake() {
            _saveDatas = new List<SaveData>();
            _filePath = Path.Combine(Application.persistentDataPath, "data.txt");
            if (_saveType == SaveType.PlayerPrefs) {
                LoadFromPlayerPrefs();
            } else {
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
            if (CurrentScoreIsNewRecord()) {
                AddCurrentScoreToTable();
                if (_saveType == SaveType.PlayerPrefs) {
                    SaveToPlayerPrefs();
                } else {
                    SaveToFile();
                }
            }
            _gameSavedEventDispatcher.Dispatch();
        }

        private bool CurrentScoreIsNewRecord() {
            if (_saveDatas.Count < _numberRecordsInTable) {
                return true;
            }
            var lastScoreInTop = Int32.Parse(_saveDatas[_saveDatas.Count - 1].score);
            return _currentScore.value > lastScoreInTop;
        }

        private void AddCurrentScoreToTable() {
            var newRecord = new SaveData {
                date = DateTime.Now.ToString("MM/dd/yyyy HH:mm"),
                score = _currentScore.value.ToString()
            };

            _saveDatas.Add(newRecord);
            _saveDatas.Sort((res1, res2) => (Int32.Parse(res2.score)).CompareTo(Int32.Parse(res1.score)));

            if (_saveDatas.Count > _numberRecordsInTable)
                _saveDatas.RemoveAt(_saveDatas.Count - 1);
        }

        private SavedDataWrapper GetWrapper() {
            var wrapper = new SavedDataWrapper {
                saveDatas = _saveDatas
            };
            return wrapper;
        }

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
                var wrapper = (SavedDataWrapper)binaryFormatter.Deserialize(fileStream);
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
    }
}