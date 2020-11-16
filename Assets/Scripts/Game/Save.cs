using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            public bool isNew;
        }

        [Serializable]
        private class SavedDataWrapper {

            public List<SaveData> savedData;
        }

        [SerializeField]
        private EventListener _carCollisionEventListener;

        [SerializeField]
        private ScriptableIntValue _currentScore;

        [SerializeField]
        private SaveType _saveType;

        private enum SaveType {
            PlayerPrefs,
            File,
        }

        private static List<SaveData> _savedData;
        public static List<SaveData> SavedData => _savedData;

        private string _filePath;

        private const string RECORDS_KEY = "records";

        private void Awake() {
            _filePath = Path.Combine(Application.persistentDataPath, "data.txt");
            _savedData = new List<SaveData>();

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
                score = _currentScore.value.ToString(),
                isNew = true
            };

            AddNewRecord(newRecord);

            if (_saveType == SaveType.File) {
                SaveToFile();
            } else {
                SaveToPlayerPrefs();
            }

            UIManager.instance.ShowLeaderBoardScreen();
        }

        private void AddNewRecord(SaveData newRecord) {
            foreach (SaveData save in _savedData) {
                save.isNew = false;
            }

            _savedData.Add(newRecord);
            _savedData = _savedData.OrderByDescending(save => Int32.Parse(save.score)).ToList<SaveData>();

            if (_savedData.Count > 10) {
                _savedData.RemoveAt(_savedData.Count - 1);
            }
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
