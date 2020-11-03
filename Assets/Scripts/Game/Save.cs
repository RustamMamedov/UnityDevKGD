using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;
using Events;


namespace Game {
    public class Save : MonoBehaviour {

        [Serializable]
        public class SaveData {

            public string date;
            public string score;
        }

        [Serializable]
        private class SavedDataWrapper {

            public List<SaveData> saveData;
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
        private SaveType _saveType;

        private static List<SaveData> _savedData;
        private const string RECORDS_KEY = "Records";
        private string _filePath;

        public static List<SaveData> SavedDatas => _savedData;

        private void Awake() {
            _savedData = new List<SaveData>();
            _filePath = Path.Combine(Application.persistentDataPath, "data.txt");
            if(_saveType == SaveType.PlayerPrefs) {
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

        private SavedDataWrapper GetWrapper() {
            var wrapper = new SavedDataWrapper {
                saveData = _savedData
            };
            return wrapper;
        }

        private void OnCarCollision() {
            var newRecord = new SaveData {
                date = DateTime.Now.ToString("MM/dd/yyyy HH:mm"),
                score = _currentScore.value.ToString()
            };
            Debug.Log($"new record: {newRecord.date} {newRecord.score}");
            _savedData.Add(newRecord);

            if (_saveType == SaveType.PlayerPrefs) {
                SaveToPlayerPrefs();
            } else {
                SaveToFile();
            }
        }

        private void LoadFromPlayerPrefs() {
            if (!PlayerPrefs.HasKey(RECORDS_KEY)) {
                return;
            }

            var wrapper = JsonUtility.FromJson<SavedDataWrapper>(PlayerPrefs.GetString(RECORDS_KEY));
            _savedData = wrapper.saveData;
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
                _savedData = wrapper.saveData;
            }
            Debug.Log(_savedData.Count);

        }

        private void SaveToFile() {
            var binaryFormatter = new BinaryFormatter();
            var wrapper = GetWrapper();
            using (FileStream fileStream = File.Open(_filePath, FileMode.OpenOrCreate)) {
                binaryFormatter.Serialize(fileStream, wrapper);
            }
        }

    }
}

