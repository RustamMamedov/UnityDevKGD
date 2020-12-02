using System;
using UnityEngine;
using UnityEngine.UI;
using Game;
using Events;

namespace UI {

    public class SettingsScreen : MonoBehaviour {

        [SerializeField]
        private Slider _volumeSlider;

        [SerializeField]
        private Toggle _difficultyToggle;

        [SerializeField]
        private Toggle _lightingToggle;

        [SerializeField]
        private Button _cancelButton;

        [SerializeField]
        private Button _acceptButton;

        [SerializeField]
        private ScriptableFloatValue _currentVolumeAsset;

        [SerializeField]
        private EventDispatcher _volumeSliderPositionChangedEventDispatcher;

        private float _volumeToSave;
        private int _difficultyToSave;
        private int _lightingToSave;


        private void Awake() {
            _acceptButton.onClick.AddListener(OnAcceptButtonClick);
            _cancelButton.onClick.AddListener(OnCancelButtonClick);
            _volumeSlider.onValueChanged.AddListener(OnVolumeSliderPositionChanged) ;
            if (!PlayerPrefs.HasKey("SavedVolume")) {
                _volumeToSave = _volumeSlider.value;
                _difficultyToSave = Convert.ToInt32(_difficultyToggle.isOn);
                _lightingToSave = Convert.ToInt32(_lightingToggle.isOn);
                PlayerPrefs.SetFloat("SavedVolume", _volumeToSave);
                PlayerPrefs.SetInt("SavedDifficulty", _difficultyToSave);
                PlayerPrefs.SetInt("SavedLight", _lightingToSave);
            }
            else {
                _volumeSlider.value = PlayerPrefs.GetFloat("SavedVolume");
                _difficultyToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("SavedDifficulty"));
                _lightingToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("SavedLight"));
            }
        }

        private void OnVolumeSliderPositionChanged(float value) {
            _currentVolumeAsset.value = value;
            _volumeSliderPositionChangedEventDispatcher.Dispatch();

        }

        private void Update() {
            _currentVolumeAsset.value = _volumeSlider.value;
        }
        private void OnAcceptButtonClick() {
            _volumeToSave = _volumeSlider.value;
            _difficultyToSave = Convert.ToInt32(_difficultyToggle.isOn);
            _lightingToSave = Convert.ToInt32(_lightingToggle.isOn);
            PlayerPrefs.SetFloat("SavedVolume", _volumeToSave);
            PlayerPrefs.SetInt("SavedDifficulty", _difficultyToSave);
            PlayerPrefs.SetInt("SavedLight", _lightingToSave);
            UIManager.Instansce.ShutDownSettingsScreen();
        }

        private void OnCancelButtonClick() {
            _currentVolumeAsset.value = PlayerPrefs.GetFloat("SavedVolume");
            _volumeSlider.value = PlayerPrefs.GetFloat("SavedVolume");
            _difficultyToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("SavedDifficulty"));
            _lightingToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("SavedLight"));
            UIManager.Instansce.ShutDownSettingsScreen();
        }

    }
}

