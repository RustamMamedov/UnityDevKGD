﻿using Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class SettingsScreen : MonoBehaviour {

        [SerializeField]
        private Button _backButton;

        [SerializeField]
        private Button _confirmButton;

        [SerializeField]
        private Slider _soundVolume;

        [SerializeField]
        private Toggle _gamemode;

        [SerializeField]
        private Toggle _daytime;

        [Serializable]
        public class SaveData {

            public float volume;
            public bool gamemode;
            public bool daytime;

        }

        [Serializable]
        private class SavedDataWrapper {

            public SaveData saveData;
        }

        private static SaveData _saveData;

        private const string RECORDS_KEY = "settings";

        private void Awake() {
            _backButton.onClick.AddListener(OnBackButtonClick);
            _confirmButton.onClick.AddListener(OnConfirmButtonClick);
        }

        private void OnEnable() {
            LoadFromPlayerPrefs();
            _soundVolume.value = _saveData.volume;
            _gamemode.isOn = _saveData.gamemode;
            _daytime.isOn = _saveData.daytime;
        }

        private void SaveForFirstTime() {
            if (PlayerPrefs.HasKey("settings")) {
                return;
            }
            var SettingsRecord = new SaveData {
                volume = 0.5f,
                gamemode = false,
                daytime = false
            };

            _saveData = SettingsRecord;
            SaveDataToPlayerPrefs();
        }

        private void OnBackButtonClick() {
            _soundVolume.value = _saveData.volume;
            _gamemode.isOn = _saveData.gamemode;
            _daytime.isOn = _saveData.daytime;
            UIManager.Instance.ShowMenuScreen();
        }

        public void OnConfirmButtonClick() {
            StartSaveProcess(_soundVolume.value, _gamemode.isOn, _daytime.isOn);
            UIManager.Instance.ShowMenuScreen();
        }

        public void StartSaveProcess(float newVolume, bool newGamemode, bool newDaytime) {

            _saveData = new SaveData {
                volume = newVolume,
                gamemode = newGamemode,
                daytime = newDaytime
            };
            SaveDataToPlayerPrefs();
        }

        private void LoadFromPlayerPrefs() {
            if (!PlayerPrefs.HasKey(RECORDS_KEY)) {
                return;
            }
            var wrapper = JsonUtility.FromJson<SavedDataWrapper>(PlayerPrefs.GetString(RECORDS_KEY));
            _saveData = wrapper.saveData;
        }

        private SavedDataWrapper GetWrapper() {
            var wrapper = new SavedDataWrapper {
                saveData = _saveData
            };
            return wrapper;
        }

        private void SaveDataToPlayerPrefs() {
            var wrapper = GetWrapper();
            var json = JsonUtility.ToJson(wrapper);
            PlayerPrefs.SetString(RECORDS_KEY, json);

        }
    }
}