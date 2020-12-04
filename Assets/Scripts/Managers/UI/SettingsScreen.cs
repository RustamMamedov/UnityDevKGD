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

        public static SettingsScreen Instance;

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
        private ScriptableIntValue _switchMode;

        [SerializeField]
        private Switch _switch;

        [SerializeField]
        private Switch _switchTime;

        [SerializeField]
        private ScriptableIntValue _switchTimeMode;

        [Serializable]
        private class SavedDataWrapper {
            public Settings saveData;
        }
        [Serializable]
        public class Settings {
            public float cooldown;
            public float volume;
            public int mode;
            public int timeMode;
            
        }
        private static Settings _saveData;
        private const string SETTINGS_KEY="settings";

        private float _volume;
        private float _cooldown;
        private int _mode;
        private int _currentMode;
        private int _timeMode;

        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            
            _volumeSlider.value = _currentVolume.value;
            _volume = (float)Math.Round(_volumeSlider.value, 1);
            _cooldown = _spawnCooldown.value;
            _currentMode = _switchMode.value;
            _mode = _currentMode;
            _timeMode = _switchTimeMode.value;
            _updateEventListener.OnEventHappened += UpdateBehaviour;
            _cancelButton.onClick.AddListener(OnCancelButtonClick);
            _oKButton.onClick.AddListener(OnOKButtonClick);
        }

        public void SetSavedValues() {
            if (!PlayerPrefs.HasKey(SETTINGS_KEY)){
                _switchMode.value = 1;
                _switchTimeMode.value = 1;
            }
            else {
                LoadFromPlayerPrefs();
                _currentVolume.value = _saveData.volume;
                _spawnCooldown.value = _saveData.cooldown;
                _switchMode.value = _saveData.mode;
                _switchTimeMode.value = _saveData.timeMode;
                
            }
        }

        private void UpdateBehaviour() {
            _volumeSlider.value = (float)Math.Round(_volumeSlider.value, 1);
            _currentVolume.value = _volumeSlider.value;
            _currentVolumeText.text = _currentVolume.value.ToString();
            if (_switchMode.value!=_currentMode) {
                if (_switchMode.value==1) {
                    _cooldown *= 2f;
                }
                else {
                    _cooldown /= 2f;
                }
                _currentMode = _switchMode.value;
            }
        }
        public void OnCancelButtonClick() {
            _currentVolume.value = _volume;
            if (_switchMode.value != _mode) {
                _switch.SwitchMode();
            }
            if (_switchTimeMode.value != _timeMode) {
                _switchTime.SwitchMode();
            }
            UIManager.Instance.ShowMenuScreen();
        }
        public void OnOKButtonClick() {
            _spawnCooldown.value = _cooldown;
            _saveData = new Settings {
                volume = _currentVolume.value,
                cooldown = _spawnCooldown.value,
                mode = _switchMode.value,
                timeMode=_switchTimeMode.value
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
