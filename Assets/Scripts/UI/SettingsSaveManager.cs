using UnityEngine;
using System;

namespace Game {

    public class SettingsSaveManager : MonoBehaviour {

        [Serializable]
        public class Settings {

            public float volume;
            public bool gameMode;
            public bool gameTime;
        }

        [SerializeField]
        private ScriptableFloatValue _volume;

        [SerializeField]
        private ScriptableBoolValue _gameMode;

        [SerializeField]
        private ScriptableBoolValue _gameTime;

        private const string SETTINGS_KEY = "Settings";

        private void Awake() {
            LoadSettings();
        }

        private void OnDestroy() {
            SaveSettings();
        }

        private void LoadSettings() {
            if (!PlayerPrefs.HasKey(SETTINGS_KEY)) {
                return;
            }

            var json = PlayerPrefs.GetString(SETTINGS_KEY);
            var settings = JsonUtility.FromJson<Settings>(json);
            SetGlobalValues(settings);
        }

        private void SetGlobalValues(Settings settings) {
            _volume.value = settings.volume;
            _gameMode.value = settings.gameMode;
            _gameTime.value = settings.gameTime;
        }

        private void SaveSettings() {
            var settings = GetCurrentSettings();
            var json = JsonUtility.ToJson(settings);
            PlayerPrefs.SetString(SETTINGS_KEY, json);
        }

        private Settings GetCurrentSettings() {
            Settings settings = new Settings {
                volume = _volume.value,
                gameMode = _gameMode.value,
                gameTime = _gameTime.value,
            };

            return settings;
        }
    }
}