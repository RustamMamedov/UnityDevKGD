using UnityEngine.UI;
using UnityEngine;
using Game;

namespace UI {

    public class SettingsScreen : MonoBehaviour {

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

        private float _lastVolumeValue;

        private void Awake() {
            _okButton.onClick.AddListener(OnOkButtonClick);
            _resumeButton.onClick.AddListener(OnResumeButtonClick);
            _volumeSlider.onValueChanged.AddListener(OnVolumeChanged);

            _lastVolumeValue = _volume.value;
        }

        private void Start() {
            TogglesTuning();
            SliderTuning();
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
            _volume.value = _volumeSlider.value;
            _gameMode.value = _hardModeToggle.isOn;
            _gameTime.value = _dayTimeToggle.isOn;

            _lastVolumeValue = _volume.value;
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
    }
}