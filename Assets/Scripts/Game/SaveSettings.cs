using Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UI;
using Sirenix.OdinInspector;

namespace Game {
    public class SaveSettings : MonoBehaviour {

        public static SaveSettings Instance;

        [Serializable]
        public class SaveData {

            public int dayornight;
            public int loworhight;
            public float volume;
        }

        [Serializable]
        private class SavedDataWrapper {

            public SaveData saveData;
        }

        [SerializeField]
        private ScriptableIntValue _dayOrNight;

        [SerializeField]
        private ScriptableIntValue _lowOrHight;

        [SerializeField]
        private Slider _volume;


        private static SaveData _saveData;
        public static SaveData SavedData => _saveData;
        private const string RECORDS_KEY = "settings";

        private void Awake() {
            Instance = this;
            LoadSave();


        }

        public void LoadSave() {
            LoadFromPlayerPrefs();
            _volume.value = _saveData.volume;
            _dayOrNight.value = _saveData.dayornight;
            _lowOrHight.value = _saveData.loworhight;
            AudioListener.volume = _volume.value;
        }
        public void StartSaveProcess(float newVolume, int day, int low) {
            _saveData = new SaveData {
                volume = newVolume,
                dayornight = day,
                loworhight = low
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