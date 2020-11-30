using System;
using Events;
using UnityEngine;
using UnityEngine.UI;
using Game;

namespace UI {

    public class SettingsScreen : MonoBehaviour {

        [SerializeField]
        private Slider _volumeSlider;

        [SerializeField]
        private Toggle _difficultToggle;

        [SerializeField]
        private Toggle _ligthToggle;

        [SerializeField]
        private Button _saveButton;

        [SerializeField]
        private Button _cancelButton;

        [SerializeField]
        private GameObject _settingsScreen;

        [SerializeField]
        private ScriptableFloatValue _volumeValue;

        private float _sliderVolumeValue;

        private const string RECORDS_KEY = "settings";

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private EventDispatcher _changeEventDispatcher;

        private const float VOLUME_DEFAULT = 0.5f;
        private const int DIFFICULT_DEFAULT = 1;
        private const int LIGHT_DEFAULT = 1;

        [Serializable]
        public class SaveData {
            public float volume;

            public int difficult;

            public int dayTime;

        }

        [Serializable]
        private class SavedDataWrapper {
            public SaveData savedData;
        }

        private static SaveData _saveData;

        private void OnEnable() {
            _updateEventListener.OnEventHappened += SetSoundVolume;
        }
        private void OnDisable() {
            SetSoundVolume();
            _updateEventListener.OnEventHappened -= SetSoundVolume;
        }

        private void Awake() {
            SaveForFirstTimeSettings();
            LoadFromPlayerPrefs();
            _saveButton.onClick.AddListener(Save);
            _cancelButton.onClick.AddListener(Cancel);
        }



        private void SetSoundVolume() {
            _volumeValue.value = _volumeSlider.value;
            _changeEventDispatcher.Dispatch();
        }

        private SavedDataWrapper GetWrapper() {
            return (new SavedDataWrapper {
                savedData = _saveData
            });
        }

        private void SaveToPlayerPrefs() {
            var wrapper = GetWrapper();
            var json = JsonUtility.ToJson(wrapper);
            PlayerPrefs.SetString(RECORDS_KEY, json);
        }

        private void LoadFromPlayerPrefs() {
            if (!PlayerPrefs.HasKey(RECORDS_KEY)) {
                return;
            }

            var wrapper = JsonUtility.FromJson<SavedDataWrapper>(PlayerPrefs.GetString(RECORDS_KEY));
            _saveData = wrapper.savedData;

            _volumeSlider.value = _saveData.volume;
            _difficultToggle.isOn = IntToBool(_saveData.difficult);
            _ligthToggle.isOn = IntToBool(_saveData.dayTime); 

            Debug.Log($"{_saveData.volume}, {_saveData.difficult}, {_saveData.dayTime}");
        }

        private void SaveForFirstTimeSettings() {
            if(PlayerPrefs.HasKey("settings")){
                return;
            }
            var SettingsRecord = new SaveData {
                volume = VOLUME_DEFAULT,
                difficult = DIFFICULT_DEFAULT,
                dayTime = LIGHT_DEFAULT
            };

            _saveData = SettingsRecord;
            SaveToPlayerPrefs();
        }

        private void Save() {

            int isDifficultToggleOn = IsON(_difficultToggle);
            int isLightToggleOn = IsON(_ligthToggle);

            var SettingsRecord = new SaveData {
                volume = _volumeSlider.value,
                difficult = isDifficultToggleOn,
                dayTime = isLightToggleOn
            };
            _volumeValue.value = _volumeSlider.value;

            _saveData = SettingsRecord;
            SaveToPlayerPrefs();
            _settingsScreen.SetActive(false);
        }

        private void Cancel() {
             Debug.Log($"{_saveData.volume}, {_saveData.difficult}, {_saveData.dayTime}");
            _volumeSlider.value = _saveData.volume;
            _difficultToggle.isOn = IntToBool(_saveData.difficult);
            _ligthToggle.isOn = IntToBool(_saveData.dayTime);
            _settingsScreen.SetActive(false);
        }
        private int IsON(Toggle toggle) {

            return toggle.isOn ? 1 : 0;
        }

        private bool IntToBool(int value) {

            return value == 1 ? true : false;
        }

    }
}