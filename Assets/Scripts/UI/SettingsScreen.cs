using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class SettingsScreen : MonoBehaviour {

        [SerializeField]
        private Slider _volumeSlider;
        [SerializeField]
        private List<AudioSource> _audioSources;

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

        private float _sliderVolumeValue;

        private const string RECORDS_KEY = "settings";

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

        private void Awake() {
            LoadFromPlayerPrefs();
            _saveButton.onClick.AddListener(Save);
        }

        private void SetSoundVolume() {

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
        }

        private void Save() {

            int isDifficultToggleOn = IsON(_difficultToggle);
            int isLightToggleOn = IsON(_ligthToggle);

            var SettingsRecord = new SaveData {
                volume = _volumeSlider.value,
                difficult = isDifficultToggleOn,
                dayTime = isLightToggleOn
            };

            _saveData = SettingsRecord;
            SaveToPlayerPrefs();
            UIManager.Instance.ShowMenuScreen();
        }

        private void Cancel() {
            _volumeSlider.value = _saveData.volume;
            _difficultToggle.isOn = IntToBool(_saveData.difficult);
            _ligthToggle.isOn = IntToBool(_saveData.dayTime);
        }
        private int IsON(Toggle toggle) {

            return toggle.isOn ? 1 : 0;
        }

        private bool IntToBool(int value) {

            return value == 1 ? true : false;
        }

    }
}