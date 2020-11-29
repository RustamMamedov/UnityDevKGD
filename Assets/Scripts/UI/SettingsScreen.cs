using System;
using Events;
using Game;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.UI;

namespace UI {
    
    public class SettingsScreen : MonoBehaviour {

        [FoldoutGroup("UI", false)]
        [SerializeField]
        private Slider _volumeSlider;

        [FoldoutGroup("UI")]
        [SerializeField] 
        private Slider _difficultySlider;
        
        [FoldoutGroup("UI")]
        [SerializeField] 
        private Slider _nightSlider;

        [FoldoutGroup("UI")]
        [SerializeField] 
        private Button _applyButton;
        
        [FoldoutGroup("UI")]
        [SerializeField] 
        private Button _cancelButton;
        
        [FoldoutGroup("Asset", false)]
        [SerializeField]
        private ScriptableFloatValue _volumeAsset;
        
        [FoldoutGroup("Asset")]
        [SerializeField]
        private ScriptableBoolValue _difficultAsset;
        
        [FoldoutGroup("Asset")]
        [SerializeField]
        private ScriptableBoolValue _nightAsset;

        [SerializeField]
        private EventDispatcher _settingsChangedDispatcher;

        [SerializeField] 
        private EventDispatcher _volumeChangedDispatcher;

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
            _settingsChangedDispatcher.Dispatch();
            UIManager.Instance.ShowMenuScreen();
        }

        private void OnCancelButtonClick() {
            _volumeAsset.value = _oldVolume;
            _difficultAsset.value = _oldDifficult;
            _nightAsset.value = _oldNight;
        }

        private void LoadUi() {
            _volumeSlider.value = _volumeAsset.value;
            _difficultySlider.value = _difficultAsset.value ? 1f : 0f;
            _nightSlider.value = _nightAsset.value ? 1f : 0f;
        }

        private void SaveCurrentState() {
            _oldVolume = _volumeAsset.value;
            _oldDifficult = _difficultAsset.value;
            _oldNight = _nightAsset.value;
        }

        private void OnVolumeSliderValueChanged(float value) {
            _volumeAsset.value = value;
        }

        private void OnDifficultySliderValueChanged(float value) {
            if (Math.Abs(value - 1.0f) < FloatComparer.kEpsilon) {
                _difficultAsset.value = true;
            } else if (Math.Abs(value - 0.0f) < FloatComparer.kEpsilon) {
                _difficultAsset.value = false;
            }
        }
        
        private void OnNightSliderValueChanged(float value) {
            if (Math.Abs(value - 1.0f) < FloatComparer.kEpsilon) {
                _nightAsset.value = true;
            } else if (Math.Abs(value - 0.0f) < FloatComparer.kEpsilon) {
                _nightAsset.value = false;
            }
        }
    }
}

