using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

namespace Game {

    public class Save : MonoBehaviour {

        [Serializable]
        public class SaveData {

            public string date;
            public string score;
        }

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
        private int PosDel;

        [Serializable]
        private class SavedDataWrapper {

            public List<SaveData> saveDatas;
        }

        public static bool finishSAve=false;

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

        [SerializeField]
        private SaveType _saveType;

        private void Awake() {
            _savedData = new List<SaveData>();
            //Debug.Log(Application.persistentDataPath);
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
            finishSAve = false;
        }

        private void OnDisable() {
            _carCollisionEventListeners.OnEventHappened -= OnCarCollison;
        }

        private void OnCarCollison() {
            //Debug.Log("SaveData");
            var newRecord = new SaveData {
                date = DateTime.Now.ToString("MM/dd/yyyy HH:mm"),
                score = _currentScore.Value.ToString()
            };
            //Debug.Log($"new record:{newRecord.date} {newRecord.score}");
            _savedData.Add(newRecord);
            if (_saveType == SaveType.PlayerPrefs) {
                SaveFromPlayerPrefs();
            }
            else {
                SaveFromFile();
            }
            finishSAve = true;
            //Debug.Log($"{_savedData[0].score}   {_savedData.Count}");
            Sort10BestResult();
            //Debug.Log($"{_savedData[0].score}   {_savedData.Count}");
        }

        private void Sort10BestResult() {
            _savedData.Sort(comparer);
            var count = _savedData.Count;
            for (int i = count-1; i >= PosDel; i--) {
                _savedData.RemoveAt(i);
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
                binaryFormatter.Serialize(fileStream,wrapper);
            }
        }

        private void LoadFromFile() {
            if (!File.Exists(_filePath)) {
                return;
            }

            var binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream=File.Open(_filePath,FileMode.OpenOrCreate)) {
                var wrapper = (SavedDataWrapper) binaryFormatter.Deserialize(fileStream);
                _savedData = wrapper.saveDatas;
            }
            //Debug.Log(_savedData.Count);
        }
    }
}
