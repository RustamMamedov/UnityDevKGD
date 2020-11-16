using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Events;
using UnityEngine;
using Sirenix.OdinInspector;

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
            File,
        }

        [SerializeField]
        private EventListener _carCollisionEventListener;

        [SerializeField]
        private ScriptableIntValue _currentScore;

        [SerializeField]
        private ScriptableIntValue _currentNumberRecord;

        [SerializeField]
        [InfoBox("@GetSaveInfo()", InfoMessageType.Info)]
        private SaveType _saveType;

        private static List<SaveData> _saveDatas;
        public static List<SaveData> SavedDatas => _saveDatas;

        private const string RECORDS_KEY = "records";
        private string _filePath;

        private void Awake() {
            _saveDatas = new List<SaveData>();
            _filePath = Path.Combine(Application.persistentDataPath, "data.txt");
            if (_saveType == SaveType.PlayerPrefs) {
                LoadFromPlayerPrefs();
            } else {
                LoadFromFile();
            }
        }

        private void OnEnable() {
            _carCollisionEventListener.OnEventHappened += OnCarCollision;
        }

        private void OnDisable() {
            _carCollisionEventListener.OnEventHappened -= OnCarCollision;
        }

        private void OnCarCollision() {
            var newRecord = new SaveData {
                date = DateTime.Now.ToString("MM/dd/yyyy HH:mm"),
                score = _currentScore.value.ToString()
            };

            if (SavedDatas.Count < 10) {
                SavedDatas.Add(newRecord);
                _currentNumberRecord.value = SavedDatas.Count - 1;
                SortSave();
            }
            else {
                _currentNumberRecord.value = -1;
                SortSave();
                if (Int32.Parse(_saveDatas[SavedDatas.Count - 1].score) < Int32.Parse(newRecord.score)) {
                    addNewRecord(newRecord);
                }
                else {
                    _currentNumberRecord.value = -1;
                }
            }

            if (_saveType == SaveType.PlayerPrefs) {
                SaveToPlayerPrefs();
            }
            else {
                SaveToFile();
            }
        }

        private void addNewRecord(SaveData newRecord) {
            _saveDatas.RemoveAt(_saveDatas.Count - 1);
            _saveDatas.Add(newRecord);
            _currentNumberRecord.value = SavedDatas.Count - 1;
            SortSave();
        }

        private void SortSave() {
            var CountDatas = _saveDatas.Count;
            for (int i = 0; i < CountDatas; i++) {
                for (int j = i; j < CountDatas; j++) {
                    if (Int32.Parse(_saveDatas[i].score) < Int32.Parse(_saveDatas[j].score)) {
                        if(_currentNumberRecord.value == j) {
                            _currentNumberRecord.value = i;
                        }

                        var tmp = _saveDatas[i].score;
                        _saveDatas[i].score = _saveDatas[j].score;
                        _saveDatas[j].score = tmp;

                        tmp = _saveDatas[i].date;
                        _saveDatas[i].date = _saveDatas[j].date;
                        _saveDatas[j].date = tmp;
                    }
                }
            }
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

        private void SaveToPlayerPrefs() {
            var wrapper = GetWrapper();
            var json = JsonUtility.ToJson(wrapper);
            PlayerPrefs.SetString(RECORDS_KEY, json);
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
            Debug.Log(_saveDatas.Count);
        }

        private void SaveToFile() {
            var wrapper = GetWrapper();
            var binaryFormatter = new BinaryFormatter();
            using(FileStream fileStream = File.Open(_filePath, FileMode.OpenOrCreate)) {
                binaryFormatter.Serialize(fileStream, wrapper);
            }
        }

        private string GetSaveInfo() {
            if (_saveType == SaveType.File) {
                return Path.Combine(Application.persistentDataPath, "records.save");
            }

            return "PlayerPrefs";
        }
    }
}