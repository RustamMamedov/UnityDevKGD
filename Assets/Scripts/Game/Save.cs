using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Events;
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

        [SerializeField] private EventListener _carCollisionEventListener;

        [SerializeField] private ScriptableIntValue _currentScore;

        [SerializeField] private SaveType _saveType;

        private List<SaveData> _saveData;
        public List<SaveData> SaveDatas => _saveData;
        private const string RECORDS_KEY = "records";
        private string _filePath;

        private void Awake() {
            _saveData = new List<SaveData>();
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
                date = DateTime.Now.ToString("MM/dd/yyyy HH:mm"),
                score = _currentScore.value.ToString(),
            };
            Debug.Log($"new record {newRecord.date} {newRecord.score} ");
            _saveData.Add(newRecord);
            if (_saveType == SaveType.PlayerPrefs) {
                SaveToPlayerPrefs();
            }
            else {
                SaveToFile();
            }
            
          
        }

        private void LoadFromPlayerPrefs() {
            if (!PlayerPrefs.HasKey(RECORDS_KEY)) {
                return;
            }

            var wrapper = JsonUtility.FromJson<SavedDataWrapper>(PlayerPrefs.GetString(RECORDS_KEY));
            _saveData = wrapper.saveDatas;
        }

        private SavedDataWrapper GetWrapper() {
            var wrapper = new SavedDataWrapper {
                saveDatas = _saveData
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
            using (FileStream fileStream = File.Open(_filePath, FileMode.Open)) {
                var wrapper = (SavedDataWrapper) binaryFormatter.Deserialize(fileStream);
                _saveData = wrapper.saveDatas;
            }

            Debug.Log(_saveData.Count);
        }

        private void SaveToFile() {
            var binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = File.Open(_filePath, FileMode.OpenOrCreate)) {
                binaryFormatter.Serialize(fileStream, GetWrapper());
            }
        }
    }
}