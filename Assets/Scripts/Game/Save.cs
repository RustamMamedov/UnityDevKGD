using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Events;
using System;
namespace Game {

    public class Save : MonoBehaviour {

        [Serializable]
        public class SaveData {
            public string date;
            public string score;
        }

        [Serializable]
        private class SavedDataWrapper {
            public List<SaveData> savedDatas;
        }

        private enum SaveType {
            PlayerPrefs,
            File
        }

        [SerializeField]
        private SaveType _saveType;

        [SerializeField]
        private EventListener _carCollisionEventListener;

        [SerializeField]
        private ScriptableIntValue _currentScore;

        private static List<SaveData> _savedDatas;

        public static List<SaveData> SavedDatas => _savedDatas;

        private const string RECORDS_KEY = "record";
        private string _filePath;

        private void Awake() {
            _savedDatas = new List<SaveData>();
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
            var NewRecord = new SaveData {
                date = DateTime.Now.ToString("MM/dd/yyyy HH:mm"),
                score = _currentScore.value.ToString()
            };
            Debug.Log($" new record: {NewRecord.date} {NewRecord.score}");
            _savedDatas.Add(NewRecord);
            if (_saveType == SaveType.PlayerPrefs) {
                SaveToPlayerPrefs();
            } else {
                SaveToFile();
            }
        }

        private SavedDataWrapper GetWrapper() {
            return (new SavedDataWrapper {
                savedDatas = _savedDatas
            });
        }
        private void LoadFromPlayerPrefs() {
            if (!PlayerPrefs.HasKey(RECORDS_KEY)) {
                return;
            }

            var wrapper = JsonUtility.FromJson<SavedDataWrapper>(PlayerPrefs.GetString(RECORDS_KEY));
            _savedDatas = wrapper.savedDatas;
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
            using (FileStream fileStream = File.Open(_filePath, FileMode.OpenOrCreate)) {
                var wrapper = (SavedDataWrapper)binaryFormatter.Deserialize(fileStream);
                _savedDatas = wrapper.savedDatas;
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