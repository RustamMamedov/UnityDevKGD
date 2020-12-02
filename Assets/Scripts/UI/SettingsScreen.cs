using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Sirenix.OdinInspector;
using Game;
using Audio;

namespace UI {

    public class SettingsScreen : MonoBehaviour {

        [Serializable]
        public class Settings {
            public float volume = 0.5f;
            public float difficulty = 0f;
            public float daytime = 0f;
        }

        [SerializeField]
        [InfoBox("$GetSaveType")]
        private Save.SaveType _saveType;
        
        [SerializeField]
        private Button _okButton;

        [SerializeField]
        private Button _cancelButton;

        [SerializeField]
        private Slider _volumeSlider;

        [SerializeField]
        private Slider _difficultySlider;

        [SerializeField]
        private Slider _daytimeSlider;

        [SerializeField]
        private ScriptableFloatValue _difficulty;

        [SerializeField]
        private ScriptableFloatValue _daytime;

        [SerializeField]
        private MusicManager _musicManager;

        private Settings _settings = new Settings();
        private const string RECORDS_KEY = "settings";
        private string _filePath;

        private void Awake() {
            _filePath = Path.Combine(Application.persistentDataPath, "settings.txt");

            if (_saveType == Save.SaveType.PlayerPrefs) {
                LoadFromPlayerPrefs();
            } else {
                LoadFromFile();
            }

            UpdateSliderValues();
            UpdateMusicVolume(_settings.volume);
        }

        private void OnEnable() {
            _okButton.onClick.AddListener(OnOkButtonClick);
            _cancelButton.onClick.AddListener(OnCancelButtonClick);
            _volumeSlider.onValueChanged.AddListener(UpdateMusicVolume);
        }

        private void OnDisable() {
            _okButton.onClick.RemoveAllListeners();
            _cancelButton.onClick.RemoveAllListeners();
            _volumeSlider.onValueChanged.RemoveAllListeners();
        }
        
        private void OnOkButtonClick() {
            SaveSettings();
            ExitToMenu();
        }

        private void OnCancelButtonClick() {
            ExitToMenu();
        }

        private void ExitToMenu() {
            UpdateSliderValues();
            ApplyGameplaySettings();
            UIManager.Instance.ShowMenuScreen();
        }

        private void SaveSettings() {
            if (_saveType == Save.SaveType.PlayerPrefs) {
                SaveToPlayerPrefs();
            } else {
                SaveToFile();
            }
        }

        private void SaveToPlayerPrefs() {
            SetNewSettings();
            var json = JsonUtility.ToJson(_settings);
            PlayerPrefs.SetString(RECORDS_KEY, json);
        }
        
        private void SaveToFile() {
            SetNewSettings();
            var binaryFormatter = new BinaryFormatter();
            using(FileStream fileStream = File.Open(_filePath, FileMode.OpenOrCreate)) {
                binaryFormatter.Serialize(fileStream, _settings);
            }
        }

        private void LoadFromPlayerPrefs() {
            if (!PlayerPrefs.HasKey(RECORDS_KEY)) {
                return;
            }

            var newSettings = JsonUtility.FromJson<Settings>(PlayerPrefs.GetString(RECORDS_KEY));
            _settings = newSettings;
        }

        private void LoadFromFile() {
            if (!File.Exists(_filePath)) {
                return;
            }

            var binaryFormatter = new BinaryFormatter();
            using (FileStream filestream = File.Open(_filePath, FileMode.OpenOrCreate)) {
                var newSettings = (Settings) binaryFormatter.Deserialize(filestream);
                _settings = newSettings;
            }
        }

        private void SetNewSettings() {
            _settings.volume = _volumeSlider.value;
            _settings.difficulty = _difficultySlider.value;
            _settings.daytime = _daytimeSlider.value;
        }

        private void ApplyGameplaySettings() {
            _difficulty.value = _settings.difficulty;
            _daytime.value = _settings.daytime;
        }

        private void UpdateSliderValues() {
            _volumeSlider.value = _settings.volume;
            _difficultySlider.value = _settings.difficulty;
            _daytimeSlider.value = _settings.daytime;
        }

        private void UpdateMusicVolume(float value) {
            _musicManager.SetVolume(value);
        }
        
        private string GetSaveType() {
            if (_saveType == Save.SaveType.PlayerPrefs) {
                return "PlayerPrefs";
            } else {
                return Application.persistentDataPath + "/settings.txt";
            }
        }
    }
}