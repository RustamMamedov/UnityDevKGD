using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Events;

namespace UI {

    public class Settings : MonoBehaviour {

        public static Settings Instance;

        [SerializeField]
        private EventDispatcher _volumeChangeEventDispatcher;

        [SerializeField]
        private Slider _volumeSlider;

        [SerializeField]
        private Scrollbar _difficultyScrollbar;

        [SerializeField]
        private Scrollbar _lightingScrollbar;

        [SerializeField]
        private float _globalVolume;

        [SerializeField]
        private bool _isEasy = false;

        [SerializeField]
        private bool _isDay = false;

        [SerializeField]
        private Button _okButton;

        [SerializeField]
        private Button _cancelButton;

        [SerializeField]
        private const string SETTINGS_SAVE_KEY = "settings";

        public List<AudioSource> audioSources = new List<AudioSource>();

        public float GlobalVolume { get => _globalVolume; }
        public bool IsEasy { get => _isEasy; }
        public bool IsDay { get => _isDay; }


        public class SaveSettings {
            public float globalVolume;
            public bool isEasy;
            public bool isDay;
        }

        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            LoadFromPlayerPrefs();
            _volumeSlider.onValueChanged.AddListener(OnVolumeChange);
            _difficultyScrollbar.onValueChanged.AddListener(OnDifficultyChange);
            _lightingScrollbar.onValueChanged.AddListener(OnLightingChange);
            _okButton.onClick.AddListener(OnOkButtonClick);
            _cancelButton.onClick.AddListener(LoadFromPlayerPrefs);
        }


        private void OnOkButtonClick() {
            SaveToPlayerPrefs();
            UIManager.Instance.ShowMenuScreen();
        }

        private void OnLightingChange(float value) {
            if (value <= 0.5) {
                _isDay = true;
            }
            else {
                _isDay = false;
            }
        }

        private void OnDifficultyChange(float value) {
            if (value <= 0.5) {
                _isEasy = true;
            }
            else {
                _isEasy = false;
            }
        }

        private void OnVolumeChange(float volume) {
            _globalVolume = volume;
            _volumeChangeEventDispatcher.Dispatch();
        }

        private SaveSettings GetSaveSettings() {
            var saveSettings = new SaveSettings {
                globalVolume = _globalVolume,
                isDay = _isDay,
                isEasy = _isEasy,
            };
            return saveSettings;
        }

        private void SaveToPlayerPrefs() {
            var saveSettings = GetSaveSettings();
            var json = JsonUtility.ToJson(saveSettings);
            PlayerPrefs.SetString(SETTINGS_SAVE_KEY, json);
        }

        private void LoadFromPlayerPrefs() {
            if (!PlayerPrefs.HasKey(SETTINGS_SAVE_KEY)) {
                _globalVolume = 0.5f;
                return;
            }

            var saveSettings = JsonUtility.FromJson<SaveSettings>(PlayerPrefs.GetString(SETTINGS_SAVE_KEY));
            _globalVolume = saveSettings.globalVolume;
            _isDay = saveSettings.isDay;
            _isEasy = saveSettings.isEasy;
            _volumeSlider.value = _globalVolume;
            _difficultyScrollbar.value = _isDay ? 0 : 1;
            _lightingScrollbar.value = _isEasy ? 0 : 1;
            _volumeChangeEventDispatcher.Dispatch();
        }



    }

}
