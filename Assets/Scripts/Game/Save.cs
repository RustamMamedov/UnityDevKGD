using Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UI;
using Sirenix.OdinInspector;

namespace Game {

    public class Save : MonoBehaviour {

        public static Save Instance;

        [Serializable]
        public class SaveData {

            public string date;
            public string score;
            public bool isHighlighted;

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
        private EventListener _carCollisionEventListener;

        [SerializeField]
        private ScriptableIntValue _currentScore;

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

        private const string RECORDS_KEY = "records";
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
            var newRecord = new SaveData {
                date = DateTime.Now.ToString("MM/dd/yyyy HH:mm"),
                score = _currentScore.value.ToString(),
                isHighlighted = true
            };
            _currentScore.value = 0;

            Turn_Highlights_Off_In_savedDatas();
            _saveDatas.Add(newRecord);
            if (!EditRecords()) {
                UIManager.Instance.ShowLeaderboardsScreen();
                return; 
            }

            if (_saveType == SaveType.PlayerPrefs) {
                SaveDataToPlayerPrefs();
            } else {
                SaveToFile();
            }
            UIManager.Instance.ShowLeaderboardsScreen();
        }
        
        private void Turn_Highlights_Off_In_savedDatas() {
            foreach (var it in _saveDatas) {
                it.isHighlighted = false;
            }
        }

        private bool EditRecords() {
            int size = _saveDatas.Count-1;
            
            if (size > 9 && Int32.Parse(_saveDatas[size-1].score) > Int32.Parse(_saveDatas[size].score)) {
                _saveDatas.RemoveAt(size);
                return false;
            }

            for (int i = size; i >0; i--) {
                if (Int32.Parse(_saveDatas[i].score) <= Int32.Parse(_saveDatas[i-1].score)) {
                    break;
                }
                SwapInSaveDatas(i, i - 1);
            }
            if (size > 9) {
                _saveDatas.RemoveAt(size);
            }

            return true;
        }

        private void SwapInSaveDatas(int x, int y) {
            SaveData temp = _saveDatas[x];
            _saveDatas[x] = _saveDatas[y];
            _saveDatas[y] = temp;
        }

        private void LoadFromPlayerPrefs() {
            if (!PlayerPrefs.HasKey(RECORDS_KEY)) {
                return;
            }

            var wrapper = JsonUtility.FromJson<SavedDataWrapper>(PlayerPrefs.GetString(RECORDS_KEY));
            _saveDatas = wrapper.saveDatas;
        }

        private SavedDataWrapper GetWrapper() {
            var wrapper = new SavedDataWrapper {
                saveDatas = _saveDatas
            };
            return wrapper;
        }

        private void SaveDataToPlayerPrefs() {
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
