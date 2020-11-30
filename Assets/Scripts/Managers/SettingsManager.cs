using Events;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using Utilities;
using Values;

namespace Managers {
    
    public class SettingsManager : SceneSingletonBase<SettingsManager> {

        // Settings subclass.

        [Serializable]
        private class GameSettings {

            // Fields.

            public int daytime;
            public int difficulty;
            public float volume;


        }


        // Constants.

        private const string _settingsKey = "settings";


        // Fields.

        [BoxGroup("Settings values")]
        [SerializeField]
        private ScriptableFloatValue _volumeValue;

        [BoxGroup("Settings values")]
        [SerializeField]
        private ScriptableIntValue _daytimeValue;

        [BoxGroup("Settings values")]
        [SerializeField]
        private ScriptableIntValue _difficultyValue;

        [BoxGroup("Event dispatchers")]
        [SerializeField]
        private EventDispatcher _volumeChangedDispatcher;

        [BoxGroup("Event dispatchers")]
        [SerializeField]
        private EventDispatcher _daytimeChangedDispatcher;

        [BoxGroup("Event dispatchers")]
        [SerializeField]
        private EventDispatcher _difficultyChangedDispatcher;

        private GameSettings _settings = new GameSettings();


        // Life cycle.

        private void Start() {
            LoadFromPlayerPrefs(out var _settings);
            if (_settings == null) {
                Save();
            } else {
                Revert();
            }
        }


        // Public methods.

        public void Revert() {
            _volumeValue.value = _settings.volume;
            _volumeChangedDispatcher.Dispatch();
            _daytimeValue.value = _settings.daytime;
            _daytimeChangedDispatcher.Dispatch();
            _difficultyValue.value = _settings.difficulty;
            _difficultyChangedDispatcher.Dispatch();
        }

        public void Save() {
            _settings = new GameSettings() {
                volume = _volumeValue.value,
                daytime = _daytimeValue.value,
                difficulty = _difficultyValue.value
            };
            SaveToPlayerPrefs(_settings);
        }


        // Saving/loading.

        private void LoadFromPlayerPrefs(out GameSettings settings) {
            if (!PlayerPrefs.HasKey(_settingsKey)) {
                settings = null;
                return;
            }
            var json = PlayerPrefs.GetString(_settingsKey);
            settings = JsonUtility.FromJson<GameSettings>(json);
        }

        private void SaveToPlayerPrefs(GameSettings settings) {
            var json = JsonUtility.ToJson(settings);
            PlayerPrefs.SetString(_settingsKey, json);
        }


    }

}
