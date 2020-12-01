using UnityEngine.UI;
using UnityEngine;
using System;
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
        private ScriptableBoolValue _hardGameMode;

        [SerializeField]
        private ScriptableBoolValue _dayTimeGame;

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

        private Settings _currentSettings;
        private const string SETTINGS_KEY = "Settings";

        private void Awake() {
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
            SetGlobalValues();
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
            _currentSettings.volume = _volumeSlider.value;
            _currentSettings.gameMode = _hardModeToggle.isOn;
            _currentSettings.gameTime = _dayTimeToggle.isOn;
        }

        private void CanselChanges() {
            SliderAndTogglesTuning();
            _volume.value = _currentSettings.volume;
        }

        private void LoadSettings() {
            if (!PlayerPrefs.HasKey(SETTINGS_KEY)) {
                _currentSettings = new Settings {
                    volume = _volume.value,
                    gameMode = _hardGameMode.value,
                    gameTime = _dayTimeGame.value,
                };
            } else {
                var json = PlayerPrefs.GetString(SETTINGS_KEY);
                _currentSettings = JsonUtility.FromJson<Settings>(json);
            }

            SetGlobalValues();
            SliderAndTogglesTuning();
        }

        private void SetGlobalValues() {
            _volume.value = _currentSettings.volume;
            _hardGameMode.value = _currentSettings.gameMode;
            _dayTimeGame.value = _currentSettings.gameTime;
        }

        private void SliderAndTogglesTuning() {
            _volumeSlider.value = _currentSettings.volume;

            if (_currentSettings.gameMode) {
                _hardModeToggle.isOn = true;
            }
            else {
                _easyModeToggle.isOn = true;
            }

            if (_currentSettings.gameTime) {
                _dayTimeToggle.isOn = true;
            }
            else {
                _nightTimeToggle.isOn = true;
            }
        }

        private void SaveSettings() {
            var json = JsonUtility.ToJson(_currentSettings);
            PlayerPrefs.SetString(SETTINGS_KEY, json);
        }
    }
}