using System;
using Game;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.UI;

namespace UI {
    
    public class SettingsScreen : MonoBehaviour {

        [SerializeField]
        private Slider _volumeSlider;

        [SerializeField] 
        private Slider _difficultySlider;
        
        [SerializeField] 
        private Slider _nightSlider;

        [SerializeField] 
        private Button _applyButton;
        
        [SerializeField] 
        private Button _cancelButton;

        private float _oldVolume;
        private bool _oldDifficult;
        private bool _oldNight;

        void Awake() {
            Debug.Log("Awake");
            SaveCurrentState();
            SubscribeToUiEvents();
            LoadUi();
        }

        private void SubscribeToUiEvents() {
            _volumeSlider.onValueChanged.AddListener(OnVolumeSliderValueChanged);
            _difficultySlider.onValueChanged.AddListener(OnDifficultySliderValueChanged);
            _nightSlider.onValueChanged.AddListener(OnNightSliderValueChanged);
            _applyButton.onClick.AddListener(OnApplyButtonClick);
            _cancelButton.onClick.AddListener(OnCancelButtonClick);
        }
        
        private void OnApplyButtonClick() {
            Settings.SaveToPlayerPrefs();
            UIManager.Instance.ShowMenuScreen();
            Debug.Log("Hi");
        }

        private void OnCancelButtonClick() {
            Settings.Volume = _oldVolume;
            Settings.IsDifficult = _oldDifficult;
            Settings.IsNight = _oldNight;
        }

        private void LoadUi() {
            _volumeSlider.value = Settings.Volume;
            _difficultySlider.value = Settings.IsDifficult ? 1f : 0f;
            _nightSlider.value = Settings.IsNight ? 1f : 0f;
        }

        private void SaveCurrentState() {
            _oldVolume = Settings.Volume;
            _oldDifficult = Settings.IsDifficult;
            _oldNight = Settings.IsNight;
        }

        private void OnVolumeSliderValueChanged(float value) {
            Settings.Volume = value;
        }

        private void OnDifficultySliderValueChanged(float value) {
            if (Math.Abs(value - 1.0f) < FloatComparer.kEpsilon) {
                Settings.IsDifficult = true;
            } else if (Math.Abs(value - 0.0f) < FloatComparer.kEpsilon) {
                Settings.IsDifficult = false;
            }
        }
        
        private void OnNightSliderValueChanged(float value) {
            if (Math.Abs(value - 1.0f) < FloatComparer.kEpsilon) {
                Settings.IsNight = true;
            } else if (Math.Abs(value - 0.0f) < FloatComparer.kEpsilon) {
                Settings.IsNight = false;
            }
        }
    }
}

