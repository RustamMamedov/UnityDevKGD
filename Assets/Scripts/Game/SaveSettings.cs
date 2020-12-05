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

            public List<SaveData> saveDatas;
        }

        private enum SaveType {

            PlayerPrefs,
            File
        }

        [SerializeField]
        private ScriptableIntValue _dayOrNight;

        [SerializeField]
        private ScriptableIntValue _lowOrHight;

        [SerializeField]
        private Slider _volume;

        [SerializeField]
        [InfoBox("PlayerPrefs", "IsPlayerPrefs")]
        [InfoBox("$FilePath", "IsFile")]
        private SaveType _saveType;

        private bool IsFile() {
            return _saveType == SaveType.File;
        }
        private bool IsPlayerPrefs() {
            return _saveType == SaveType.PlayerPrefs;
        }

        private static List<SaveData> _saveDatas;
        public static List<SaveData> SavedDatas => _saveDatas;
        private const string RECORDS_KEY = "settings";
        private string _filePath;
        private string FilePath => Path.Combine(Application.persistentDataPath, "data.txt");

        private void Awake() {
            _saveDatas = new List<SaveData>();
            _filePath = Path.Combine(Application.persistentDataPath, "data.txt");

            if (_saveType == SaveType.PlayerPrefs) {
                LoadFromPlayerPrefs();
            } else {
                LoadFromFile();
            }

            if (Instance != null) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void StartSaveProcess() {
            var newSet = new SaveData {
                dayornight = _dayOrNight.value,
                loworhight = _lowOrHight.value,
                volume = _volume.value
            };
            Debug.Log($"{ _volume.value }");
            _saveDatas.Add(newSet);
            if (_saveType == SaveType.PlayerPrefs) {
                SaveDataToPlayerPrefs();
            } else {
                SaveToFile();
            }

        }
        private void LoadFromPlayerPrefs() {
            Debug.Log("load");
            if (!PlayerPrefs.HasKey(RECORDS_KEY)) {
                return;
            }

            var wrapper = JsonUtility.FromJson<SavedDataWrapper>(PlayerPrefs.GetString(RECORDS_KEY));
            _saveDatas = wrapper.saveDatas;
            Debug.Log($"{_volume.value}");
        }

        private SavedDataWrapper GetWrapper() {
            var wrapper = new SavedDataWrapper {
                saveDatas = _saveDatas
            };
            return wrapper;
        }

        private void SaveDataToPlayerPrefs() {
            Debug.Log("save");
            var wrapper = GetWrapper();
            var json = JsonUtility.ToJson(wrapper);
            PlayerPrefs.SetString(RECORDS_KEY, json);
        }

        private void LoadFromFile() {
            if (!File.Exists(_filePath)) {
                return;
            }

            var binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = File.Open(_filePath, FileMode.Open)) {
                var wrapper = (SavedDataWrapper)binaryFormatter.Deserialize(fileStream);
                _saveDatas = wrapper.saveDatas;
            }
        }

        private void SaveToFile() {
            var wrapper = GetWrapper();

            var binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = File.Open(_filePath, FileMode.OpenOrCreate)) {
                binaryFormatter.Serialize(fileStream, wrapper);
            }
        }

    }
}