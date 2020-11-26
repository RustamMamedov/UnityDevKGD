using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Events;
using Sirenix.OdinInspector;
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
        private EventListener _gameSavedEvent;

        [SerializeField]
        private SettingsScreen _settingsScreen;

        [SerializeField]
        private ScriptableIntValue _currentScore;

        [InfoBox("@\"Path: \" + Path.Combine(UnityEngine.Application.persistentDataPath, \"data.txt\")", "IsSaveTypeFile")]
        [InfoBox("Player prefs", "IsSaveTypePlayerPrefs")]
        [SerializeField]
        private SaveType _saveType;

        public class SavedSettings {
            public float volumeValue;
            public Time time;
            public Difficulty difficulty;

            public enum Time {
                Day,
                Night,
            }

            public enum Difficulty {
                Easy,
                Hard,
            }
        }

        private enum SaveType {
            PlayerPrefs,
            File,
        }

        private static SavedSettings _savedSettings;
        public static SavedSettings Settings => _savedSettings;

        private static List<SaveData> _savedData;
        public static List<SaveData> SavedData => _savedData;

        private const string RECORDS_KEY = "records";
        private const string SETTINGS_KEY = "settings";

        private string _filePath;

        private void Awake() {
            _filePath = Path.Combine(Application.persistentDataPath, "data.txt");
            _savedData = new List<SaveData>();
            _savedSettings = new SavedSettings();

            LoadSettings();

            if (_saveType == SaveType.PlayerPrefs) {
                LoadFromPlayerPrefs();
            } else {
                LoadFromFile();
            }

        }

        private void OnEnable() {
            _carCollisionEventListener.OnEventHappened += OnCarCollision;
            _gameSavedEvent.OnEventHappened += OnGameSaved;
        }

        private void OnDisable() {
            _carCollisionEventListener.OnEventHappened -= OnCarCollision;
            _gameSavedEvent.OnEventHappened -= OnGameSaved;
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

        private void OnGameSaved() {
            var settings = new SavedSettings {
                volumeValue = _settingsScreen.Volume,
                time = _settingsScreen.Time > 0.5 ? SavedSettings.Time.Night : SavedSettings.Time.Day,
                difficulty = _settingsScreen.Difficulty > 0.5 ? SavedSettings.Difficulty.Hard : SavedSettings.Difficulty.Easy,
            };

            var json = JsonUtility.ToJson(settings);
            PlayerPrefs.SetString(SETTINGS_KEY, json);

            _savedSettings = settings;
        }

        private void LoadSettings() {
            if (!PlayerPrefs.HasKey(SETTINGS_KEY)) {
                return;
            }

            var settings = JsonUtility.FromJson<SavedSettings>(PlayerPrefs.GetString(SETTINGS_KEY));
            _savedSettings = settings;
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

        private bool IsSaveTypeFile(SaveType saveType) {
            return saveType == SaveType.File;
        }

        private bool IsSaveTypePlayerPrefs(SaveType saveType) {
            return saveType == SaveType.PlayerPrefs;
        }
    }
}
