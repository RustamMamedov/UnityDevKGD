using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using Events;
using UI;
using UnityEngine;

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
            File,
        }

        [SerializeField]
        private EventListener _carCollisionEventListener;

        [SerializeField]
        private ScriptableIntValue _currentScore;

        [SerializeField]
        private SaveType _saveType;

        [SerializeField]
        private ScriptableIntValue _currentScorePosition;
        
        [SerializeField] 
        private ScriptableBoolValue _crazyModeEnabled;
        
        private static List<SaveData> _saveDatas;
        public static List<SaveData> SavedDatas => _saveDatas;

        private const string RECORDS_KEY = "records";
        private string _filePath;
        private bool _saved = false;
            
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
            var newRecord = new SaveData {
                date = DateTime.Now.ToString("MM/dd/yyyy HH:mm"),
                score = _currentScore.value.ToString()
            };
            _saveDatas.Add(newRecord);
            SortListAndLeaveTenEntries();
            if (_saveType == SaveType.PlayerPrefs) {
                SaveToPlayerPrefs();
            } else {
                SaveToFile();
            }
            UIManager.Instance.ShowLeaderboardScreen();
        }

        private void SortListAndLeaveTenEntries() {
            _currentScorePosition.value = 11;
            
            if (_saveDatas.Count <= 1) {
                return;
            }
            
            for (int i = 0; i < _saveDatas.Count; i++) {
                for (int j = i + 1; j < _saveDatas.Count; j++) {
                    if (_saveDatas[i].score == "0") {
                        _saveDatas.Remove(_saveDatas[i]);
                    }
                    
                    if (Int32.Parse(_saveDatas[i].score) < Int32.Parse(_saveDatas[j].score)) {
                        var tmp = _saveDatas[i];
                        _saveDatas[i] = _saveDatas[j];
                        _saveDatas[j] = tmp;
                    }
                    
                    if (_saveDatas[i].score == _saveDatas[j].score) {
                        _saveDatas.Remove(_saveDatas[i]);
                    }

                    if (Int32.Parse(_saveDatas[i].score) == _currentScore.value) {
                        _currentScorePosition.value = i;
                    }
                }
            }
            if (_saveDatas.Count > 10) {
                for (int i = _saveDatas.Count - 1; i > 9; i--) {
                    _saveDatas.Remove(_saveDatas[i]);
                }
            }
        }

        private void LoadFromPlayerPrefs() {
            if (!PlayerPrefs.HasKey(RECORDS_KEY)) {
                return;
            }

            var wrapper = JsonUtility.FromJson<SavedDataWrapper>(PlayerPrefs.GetString(RECORDS_KEY));
            _saveDatas = wrapper.saveDatas;
        }

        private SavedDataWrapper GetWrapper() {
            var wrapper = new SavedDataWrapper {
                saveDatas = _saveDatas
            };
            return wrapper;
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
            using(FileStream fileStream = File.Open(_filePath, FileMode.Open)) {
                var wrapper = (SavedDataWrapper) binaryFormatter.Deserialize(fileStream);
                _saveDatas = wrapper.saveDatas;
            }
        }

        private void SaveToFile() {
            //TODO: create two different files: for casual and crazy mode
            var wrapper = GetWrapper();
            var binaryFormatter = new BinaryFormatter();
            using(FileStream fileStream = File.Open(_filePath, FileMode.OpenOrCreate)) {
                binaryFormatter.Serialize(fileStream, wrapper);
            }
        }
    }
}