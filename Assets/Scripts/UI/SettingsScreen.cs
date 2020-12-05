using UnityEngine.UI;
using UnityEngine;
using System;
using Game;

namespace UI {

    public class SettingsScreen : MonoBehaviour {

    public class Settings {
        public float volume;
        public bool gameMode;
        public bool gameTime;
    }

    [SerializeField]
        private Button _okButton;

        [SerializeField]
        private Button _cancelButton;

        [SerializeField]
        private Slider _volumeSlider;

        [SerializeField]
        private ScriptableFloatValue _volume;

        [SerializeField]
        private ScriptableBoolValue _gameMode;

        [SerializeField]
        private ScriptableBoolValue _timeGame;

        [SerializeField]
        private Toggle _easyMode;

        [SerializeField]
        private Toggle _hardMode;

        [SerializeField]
        private Toggle _dayTime;

        [SerializeField]
        private Toggle _nightTime;

        private const string SETTINGS_KEY = "Settings";
        private float _lastVolumeValue;

        private void Awake() {
            _lastVolumeValue = _volume.value;
            _okButton.onClick.AddListener(OnOkButtonClick);
            _cancelButton.onClick.AddListener(OnCancelButtonClick);
            _volumeSlider.onValueChanged.AddListener(OnVolumeChanged);

            LoadSettings();
        }

        private void OnDestroy() {
            SaveSettings();
        }

        private void OnOkButtonClick() {
            SaveChanges();
            UIManager.Instance.ShowMenuScreen();
        }

        private void OnCancelButtonClick() {
            CanselChanges();
            UIManager.Instance.ShowMenuScreen();
        }

        private void OnVolumeChanged(float value) {
            _volume.value = value;
        }

        private void SaveChanges() {
            _lastVolumeValue = _volume.value;
            _volume.value = _volumeSlider.value;
            _gameMode.value = _hardMode.isOn;
            _timeGame.value = _dayTime.isOn;
        }

        private void CanselChanges() {
            TogglesTuning();
            _volume.value = _lastVolumeValue;
            SliderTuning();
        }

        private void LoadSettings() {
            if (!PlayerPrefs.HasKey(SETTINGS_KEY)) {
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
            _timeGame.value = settings.gameTime;
        }

        private void TogglesTuning() {
            if (_gameMode.value) {
                _hardMode.isOn = true;
            } else {
                _easyMode.isOn = true;
            }

            if (_timeGame.value) {
                _dayTime.isOn = true;
            } else {
                _nightTime.isOn = true;
            }
        }

        private void SliderTuning() {
            _volumeSlider.value = _volume.value;
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
                gameTime = _timeGame.value,
            };
            return settings;
        }
    }
}