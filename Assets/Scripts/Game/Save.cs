using UnityEngine;
using Events;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine.Jobs;

namespace Game {

    public class Save : MonoBehaviour {

        [Serializable]
        public class SaveData {

            public string date;
            public string score;
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
        private SaveType _saveType;

        [SerializeField]
        private int _countBestResult;

        [SerializeField]
        private EventDispatcher _endingSave;

        private static List<SaveData> _saveDatas;
        public static List<SaveData> savedDatas => _saveDatas;

        private const string RECORDS_KAY = "records";
        private string _filePath;
        public static int _last;

        private void Awake() {
            _saveDatas = new List<SaveData>();
            _filePath = Path.Combine(Application.persistentDataPath, "date.txt");

            if (_saveType == SaveType.PlayerPrefs) {
                LoadFromPlayerPrefs();
            } else {
                LoadFromFile();
            }
            //Debug.Log(Application.persistentDataPath);
        }

        private void OnEnable() {
            _carCollisionEventListener.OnEventHappened += OnCarCollision;
        }

        private void OnDisable() {
            _carCollisionEventListener.OnEventHappened -= OnCarCollision;
        }

        private void BestResults(SaveData record) { 
            for (int i = 0; i<_saveDatas.Count-1; i++) {
                var value1 = Int32.Parse(_saveDatas[i].score);
                for (int j = i+1; j < _saveDatas.Count; j++) {
                    var value2 = Int32.Parse(_saveDatas[j].score);
                    if(value1 < value2) {
                        var temp = new SaveData();
                        temp = _saveDatas[i];
                        _saveDatas[i] = _saveDatas[j];
                        _saveDatas[j] = temp;
                    }
                }
            }
            for (int i = 0; i < _saveDatas.Count; i++) {
                if (record == _saveDatas[i]) {
                    _last = i;
                    Debug.Log($"{_last} zzz");
                    break;
                }
            }
            while (_saveDatas.Count > _countBestResult) {
                _saveDatas.RemoveAt(_saveDatas.Count-1);
            }
        }

        private void OnCarCollision() {
            var newRecord = new SaveData {
                date = DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                score = _currentScore.value.ToString()
            };
            //Debug.Log($"new record: {newRecord.date} {newRecord.score}");
            _saveDatas.Add(newRecord);
            BestResults(newRecord);
            if (_saveType == SaveType.PlayerPrefs) {
                SaveToPlayerPrefs();
            }
            else {
                SaveToFile();
            }
            _endingSave.Dispatch();
        }



        private void LoadFromPlayerPrefs() {
            if (!PlayerPrefs.HasKey(RECORDS_KAY)) {
                return;
            }

            var wrapper = JsonUtility.FromJson<SavedDataWrapper>(PlayerPrefs.GetString(RECORDS_KAY));
            _saveDatas = wrapper.saveDatas;
            //Debug.Log(_saveDatas.Count);
        }

        private SavedDataWrapper GetWrapper() {
            var wrapper = new SavedDataWrapper {
                saveDatas = _saveDatas
            };
            return wrapper;
        }

        private void SaveToPlayerPrefs() {
            var wrapper = GetWrapper();
            var json = JsonUtility.ToJson(wrapper);
            PlayerPrefs.SetString(RECORDS_KAY, json);
        }

        private void LoadFromFile() {
            if (!File.Exists(_filePath)) {
                return;
            }

            var binaryFormatter = new BinaryFormatter();
            using(FileStream fileStream = File.Open(_filePath, FileMode.Open)) {
                var wrapper = (SavedDataWrapper) binaryFormatter.Deserialize(fileStream);
                _saveDatas = wrapper.saveDatas;
            }
            //Debug.Log(_saveDatas.Count);
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

