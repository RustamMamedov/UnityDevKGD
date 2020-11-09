using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Game {
    public class Save : MonoBehaviour {

        [SerializeField]
        public class SaveData {
            public string date;
            public string score;
        }

        private class ScoreComparer : IComparer<SaveData> {
            public int Compare(SaveData d1, SaveData d2) {
                var val1 = Int32.Parse(d1.score);
                var val2 = Int32.Parse(d2.score);
                if (val1 > val2) {
                    return -1;
                }
                else if (val1 < val2) {
                    return 1;
                }
                return 0;
            }
        }

        private ScoreComparer _comparer;

        [SerializeField]
        private class SavedDataWrapper {
            public List<SaveData> saveDatas;
        }

        private enum SaveType {
            PlayerPrefs,
            File
        }

        [SerializeField]
        private EventListeners _carCollisionEventListener;

        [SerializeField]
        private ScriptableIntValue _currentScore;

        [SerializeField]
        private SaveType _saveType;
        [SerializeField]
        private int _countBestResult;

        private static List<SaveData> _saveDatas;
        public static List<SaveData> SavedDatas => _saveDatas;

        private const string RECORDS_KEY = "records";
        private string _filePath;

        private void Awake() {
            _saveDatas = new List<SaveData>();
            _filePath = Path.Combine(Application.persistentDataPath,"data.txt");
            if (_saveType == SaveType.PlayerPrefs) {
                LoadFromPlayerPrefs();
            }
            else {
                LoadFromFile();
            }
        }

        private void OnEnable() {
            _carCollisionEventListener.OnEventHappened += OnCarCollision;
        }
        private void OnDisable() {
            _carCollisionEventListener.OnEventHappened -= OnCarCollision;
            _saveDatas.Clear();
        }

        private void BestResults() {
            _saveDatas.Sort(_comparer);
            for(int i = _saveDatas.Count - 1; i >= _countBestResult; i--) {
                _saveDatas.RemoveAt(i);
            }
        }
        private void OnCarCollision() {
            var newRecord = new SaveData {
                date = DateTime.Now.ToString("MM/dd/yyyy HH:mm"),
                score = _currentScore.value.ToString()
            };
            _saveDatas.Add(newRecord);

            if (_saveType == SaveType.PlayerPrefs) {
                SaveToPlayerPrefs();
            }
            else {
                SaveToFile();
            }
            BestResults();

        }

        private SavedDataWrapper GetWrapper() {
            var wrapper = new SavedDataWrapper {
                saveDatas = _saveDatas
            };
            return wrapper;
        }

        private void LoadFromPlayerPrefs() {
            if (!PlayerPrefs.HasKey(RECORDS_KEY)) {
                return;
            }
            var wrapper = JsonUtility.FromJson<SavedDataWrapper>(PlayerPrefs.GetString(RECORDS_KEY));
            _saveDatas = wrapper.saveDatas;
        }
        private void SaveToPlayerPrefs() {
            var wrapper = GetWrapper();
            var json = JsonUtility.ToJson(wrapper);
            PlayerPrefs.SetString(RECORDS_KEY, json);
        }

        private void LoadFromFile() {
            if (File.Exists(_filePath)) {
                return;
            }
            var binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = File.Open(_filePath, FileMode.OpenOrCreate)) {
                var wrapper = (SavedDataWrapper)binaryFormatter.Deserialize(fileStream);
                _saveDatas = wrapper.saveDatas;
            }
            Debug.Log(_saveDatas.Count);
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