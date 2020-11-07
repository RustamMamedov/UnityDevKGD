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

            public List<SaveData> savedData;
        }

        [SerializeField]
        private EventListener _carCollisionEventListener;

        [SerializeField]
        private ScriptableIntValue _currentScore;

        private List<SaveData> _savedData;
        public List<SaveData> SavedData => _savedData;

        private string _filePath;

        private const string RECORDS_KEY = "records";

        private void Awake() {
            _filePath = Path.Combine(Application.persistentDataPath, "data.txt");
            _savedData = new List<SaveData>();
            //LoadFromPlayerPrefs();
            LoadFromFile();
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

            _savedData.Add(newRecord);
            //SaveToPlayerPrefs();
            SaveToFile();
        }

        private void LoadFromPlayerPrefs() {
            if (!PlayerPrefs.HasKey(RECORDS_KEY)) {
                return;
            }

            var wrapper = JsonUtility.FromJson<SavedDataWrapper>(PlayerPrefs.GetString(RECORDS_KEY));
            _savedData = wrapper.savedData;
        }

        private void SaveToPlayerPrefs() {
            var json = JsonUtility.ToJson(GetWrapper());
            PlayerPrefs.SetString(RECORDS_KEY, json);
        }

        private void LoadFromFile() {
            if (!File.Exists(_filePath)) {
                return;
            }

            var binaryFormatter = new BinaryFormatter();
            using(FileStream fileStream = File.Open(_filePath, FileMode.OpenOrCreate)) {
                var wrapper = (SavedDataWrapper) binaryFormatter.Deserialize(fileStream);
                _savedData = wrapper.savedData;
            }
        }

        private void SaveToFile() {
            var binaryFormatter = new BinaryFormatter();
            using(FileStream fileStream = File.Open(_filePath, FileMode.OpenOrCreate)) {
                binaryFormatter.Serialize(fileStream, GetWrapper());
            }
        }

        private SavedDataWrapper GetWrapper() {
            return new SavedDataWrapper {
                savedData = _savedData
            };
        }
    }
}
