using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Sirenix.OdinInspector;

namespace Game {

    public class Save : MonoBehaviour {
        [SerializeField]
        private EventDispatcher _saveResultEventDispatcher;

        [Serializable]
        public class SaveData {

            public string date;
            public string score;
        }

        public static int last;

        //for Sort
        private class ScoreComparer : IComparer<SaveData> {

            public int Compare(SaveData a1, SaveData a2) {
                var value1 = Int32.Parse(a1.score);
                var value2 = Int32.Parse(a2.score);
                if (value1 > value2) {
                    return -1;
                }
                else if (value1 < value2) {
                    return 1;
                }

                return 0;
            }
        }

        private ScoreComparer comparer = new ScoreComparer();

        [SerializeField]
        private int _countBestResults;

        [Serializable]
        private class SavedDataWrapper {

            public List<SaveData> saveDatas;
        }

        [SerializeField]
        private EventListeners _carCollisionEventListeners;

        [SerializeField]
        private ScriptableIntValue _currentScore;

        private static List<SaveData> _savedData;

        public static List<SaveData> SavedDatas => _savedData;

        private const string Records_Key = "records";

        private string _filePath;

        private enum SaveType {
            PlayerPrefs,
            FromFile
        }

        [InfoBox("$_information",nameof (Information))]
        [SerializeField]
        private SaveType _saveType;
        private string _information;
        private bool Information() {
            if (_saveType == SaveType.FromFile) {
                _information = Path.Combine(Application.persistentDataPath, "data.txt");
            }
            else {
                _information = "PlayerPrefs";
            }
            return true;
        }

        private void Awake() {
            _savedData = new List<SaveData>();
            _filePath = Path.Combine(Application.persistentDataPath, "data.txt");
            if (_saveType == SaveType.PlayerPrefs) {
                LoadFromPlayerPrefs();
            }
            else {
                LoadFromFile();
            }
        }

        private void OnEnable() {
            _carCollisionEventListeners.OnEventHappened += OnCarCollison;
        }

        private void OnDisable() {
            _carCollisionEventListeners.OnEventHappened -= OnCarCollison;   
            _savedData.Clear();
        }

        private void OnCarCollison() {
            var newRecord = new SaveData {
                date = DateTime.Now.ToString("MM/dd/yyyy HH:mm"),
                score = _currentScore.value.ToString()

            };
            //Debug.Log($"new record:{newRecord.date} {newRecord.score}");
            _savedData.Add(newRecord);
            if (_saveType == SaveType.PlayerPrefs) {
                SaveFromPlayerPrefs();
            }
            else {
                SaveFromFile();
            }
            //Debug.Log("SaveData");
            //Debug.Log($"{_savedData[0].score}   {_savedData.Count}");
            Sort10BestResult(newRecord);
            //Debug.Log($"{_savedData[0].score}   {_savedData.Count}");
            _saveResultEventDispatcher.Dispatch();
        }

        private void Sort10BestResult(SaveData newResult) {
            _savedData.Sort(comparer);
            var count = _savedData.Count;
            for (int i = count - 1; i >= _countBestResults; i--) {
                _savedData.RemoveAt(i);

            }
            for (int i = 0; i < _savedData.Count; i++) {
                if (newResult == _savedData[i]) {
                    last = i;
                    break;
                }
                else last = -1;
            }
        }

        private void LoadFromPlayerPrefs() {
            if (!PlayerPrefs.HasKey(Records_Key)) {
                return;
            }
            var wrapper = JsonUtility.FromJson<SavedDataWrapper>(PlayerPrefs.GetString(Records_Key));
            _savedData = wrapper.saveDatas;

        }

        private SavedDataWrapper GetDataWrapper() {
            var wrapper = new SavedDataWrapper {
                saveDatas = _savedData
            };
            return wrapper;
        }

        private void SaveFromPlayerPrefs() {
            var wrapper = GetDataWrapper();
            var json = JsonUtility.ToJson(wrapper);
            //Debug.Log(json);
            PlayerPrefs.SetString(Records_Key, json);
        }

        private void SaveFromFile() {
            var wrapper = GetDataWrapper();

            var binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = File.Open(_filePath, FileMode.OpenOrCreate)) {
                binaryFormatter.Serialize(fileStream, wrapper);
            }
        }

        private void LoadFromFile() {
            if (!File.Exists(_filePath)) {
                return;
            }

            var binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = File.Open(_filePath, FileMode.OpenOrCreate)) {
                var wrapper = (SavedDataWrapper)binaryFormatter.Deserialize(fileStream);
                _savedData = wrapper.saveDatas;
            }
            //Debug.Log(_savedData.Count);
        }
    }
}
