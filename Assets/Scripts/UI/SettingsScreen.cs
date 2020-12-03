using UnityEngine.UI;
using UnityEngine;
using System;
using Audio;
using Game;

namespace UI {

    public class SettingsScreen : MonoBehaviour {

        [Serializable]
        public class Settings {

            public float volume;
            public bool gameMode;
            public bool gameTime;
        }

        [SerializeField]
        private Button _okButton;

        [SerializeField]
        private Button _resumeButton;

        [SerializeField]
        private Slider _volumeSlider;

        [SerializeField]
        private ScriptableFloatValue _volume;

        [SerializeField]
        private ScriptableBoolValue _gameMode;

        [SerializeField]
        private ScriptableBoolValue _gameTime;

        #region Toggles

        [SerializeField]
        private Toggle _easyModeToggle;

        [SerializeField]
        private Toggle _hardModeToggle;

        [SerializeField]
        private Toggle _dayTimeToggle;

        [SerializeField]
        private Toggle _nightTimeToggle;

        #endregion Toggles

        private const string SETTINGS_KEY = "Settings";
        private float _lastVolumeValue;

        private void Awake() {
            _lastVolumeValue = _volume.value;
            _okButton.onClick.AddListener(OnOkButtonClick);
            _resumeButton.onClick.AddListener(OnResumeButtonClick);
            _volumeSlider.onValueChanged.AddListener(OnVolumeChanged);

            LoadSettings();
        }

        private void OnDestroy() {
            SaveSettings();
        }

        private void OnOkButtonClick() {
            AppleChanges();
            UIManager.Instance.ShowMenuScreen();
        }

        private void OnResumeButtonClick() {
            CanselChanges();
            UIManager.Instance.ShowMenuScreen();
        }

        private void OnVolumeChanged(float value) {
            _volume.value = value;
        }

        private void AppleChanges() {
            _lastVolumeValue = _volume.value;

            _volume.value = _volumeSlider.value;
            _gameMode.value = _hardModeToggle.isOn;
            _gameTime.value = _dayTimeToggle.isOn;
        }

        private void CanselChanges() {
            TogglesTuning();
            _volume.value = _lastVolumeValue;
            SliderTuning();
        }

        private void TogglesTuning() {
            if (_gameMode.value) {
                _hardModeToggle.isOn = true;
            } else {
                _easyModeToggle.isOn = true;
            }

            if (_gameTime.value) {
                _dayTimeToggle.isOn = true;
            } else {
                _nightTimeToggle.isOn = true;
            }
        }

        private void SliderTuning() {
            _volumeSlider.value = _volume.value;
        }

        private void LoadSettings() {
            if (PlayerPrefs.HasKey(SETTINGS_KEY)) {
                var json = PlayerPrefs.GetString(SETTINGS_KEY);
                var settings = JsonUtility.FromJson<Settings>(json);
                SetGlobalValues(settings);
            }

            TogglesTuning();
            SliderTuning();
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