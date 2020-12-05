using Events;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Game;

namespace UI {

    public class SettingsSave : MonoBehaviour {

        public static SettingsSave Instance;

        [Serializable]
        public class SaveData {

            public float volume;
            public bool gamemode;
            public bool daytime;

        }

        [Serializable]
        private class SavedDataWrapper {

            public SaveData saveData;
        }

        private static SaveData _saveData;

        private const string RECORDS_KEY = "settings";


        private void Awake() {
            _saveData = new SaveData { 
                volume = 0.5f,
                gamemode = false,
                daytime = false
            };

            LoadFromPlayerPrefs();
            
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public float getVolume() {
            return _saveData.volume;
        }

        public bool getGamemode() {
            return _saveData.gamemode;
        }

        public bool getDayTime() {
            return _saveData.daytime;
        }

        public void StartSaveProcess(float newVolume, bool newGamemode, bool newDaytime) {

            _saveData = new SaveData {
                volume = newVolume,
                gamemode = newGamemode,
                daytime = newDaytime
            };
            SaveDataToPlayerPrefs();
        }   

        private void LoadFromPlayerPrefs() {
            if (!PlayerPrefs.HasKey(RECORDS_KEY)) {
                return;
            }

            var wrapper = JsonUtility.FromJson<SavedDataWrapper>(PlayerPrefs.GetString(RECORDS_KEY));
            _saveData = wrapper.saveData;
        }

        private SavedDataWrapper GetWrapper() {
            var wrapper = new SavedDataWrapper {
                saveData = _saveData
            };
            return wrapper;
        }

        private void SaveDataToPlayerPrefs() {
            var wrapper = GetWrapper();
            var json = JsonUtility.ToJson(wrapper);
            PlayerPrefs.SetString(RECORDS_KEY, json);
        }

        public static SettingsSave GetInstance() {
            if (Instance == null) {
                Instance = new SettingsSave();
            }

            return Instance;
        }

    }
}
