using System.Collections;
using System.Collections.Generic;
using Audio;
using Game;
using System;
using Events;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class SettingsScreen : MonoBehaviour {

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private Slider _volumeSlider;

        [SerializeField]
        private Text _currentVolumeText;

        [SerializeField]
        private Button _oKButton;

        [SerializeField]
        private Button _cancelButton;

        [SerializeField]
        private ScriptableFloatValue _currentVolume;

        [SerializeField]
        private ScriptableFloatValue _spawnCooldown;

        [SerializeField]
        private Switch _switchMode;

        [Serializable]
        private class SavedDataWrapper {
            public Settings saveData;
        }
        [Serializable]
        public class Settings {
            public float cooldown;
            public float volume;
            public bool mode;

            
        }
        private static Settings _saveData;
        private const string SETTINGS_KEY="settings";

        private float _volume;
        private float _cooldown;
        private bool _mode;
        private bool _currentMode;

        private void Awake() {
            LoadFromPlayerPrefs();
            _currentVolume.value = _saveData.volume;
            _spawnCooldown.value = _saveData.cooldown;
            _volumeSlider.value = _currentVolume.value;
            _volume = (float)Math.Round(_volumeSlider.value, 1);
            _cooldown = _spawnCooldown.value;
            _currentMode = _switchMode.isOn;
            _mode = _currentMode;
            _updateEventListener.OnEventHappened += UpdateBehaviour;
            _cancelButton.onClick.AddListener(OnCancelButtonClick);
            _oKButton.onClick.AddListener(OnOKButtonClick);
        }


        private void UpdateBehaviour() {
            _volumeSlider.value = (float)Math.Round(_volumeSlider.value, 1);
            _currentVolume.value = _volumeSlider.value;
            _currentVolumeText.text = _currentVolume.value.ToString();
            if (_switchMode.isOn != _currentMode) {
                if (!_switchMode.isOn) {
                    _cooldown *= 2.0f;
                }
                else {
                    _cooldown /= 2.0f;
                }
                _currentMode = _switchMode.isOn;
            }
            
        }
        public void OnCancelButtonClick() {
            _currentVolume.value = _volume;
            if (_switchMode.isOn != _mode) {
                _switchMode.SwitchMode(_switchMode.isOn);
            }
            UIManager.Instance.ShowMenuScreen();
        }
        public void OnOKButtonClick() {
            _spawnCooldown.value = _cooldown;
            _saveData = new Settings {
                volume = _currentVolume.value,
                cooldown = _spawnCooldown.value,
                mode = _switchMode.isOn
        };
            
            SaveToPlayerPrefs();
            UIManager.Instance.ShowMenuScreen();
        }

        private SavedDataWrapper GetWrapper() {
            var wrapper = new SavedDataWrapper {
                saveData = _saveData
            };
            return wrapper;
        }
        private void LoadFromPlayerPrefs() {
            if (!PlayerPrefs.HasKey(SETTINGS_KEY)) {
                return;
            }
            var wrapper = JsonUtility.FromJson<SavedDataWrapper>(PlayerPrefs.GetString(SETTINGS_KEY));
            _saveData = wrapper.saveData;
        }
        private void SaveToPlayerPrefs() {
            var wrapper = GetWrapper();
            var json = JsonUtility.ToJson(wrapper);
            PlayerPrefs.SetString(SETTINGS_KEY, json);
        }
    }
}
