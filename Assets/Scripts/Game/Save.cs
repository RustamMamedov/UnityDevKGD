using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Events;
using System;
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
            public List<SaveData> savedDatas;
        }

        private enum SaveType {
            PlayerPrefs,
            File
        }

        [SerializeField]
        [InfoBox("PlayerPrefs", nameof(IsSaveTypePlayerPrefs))]
        [InfoBox("C:/Users/PC/AppData/LocalLow/DefaultCompany/UnityDev2020/data.txt", nameof(IsSaveTypeFile))]
        private SaveType _saveType;

        [SerializeField] private EventListener _carCollisionEventListener;

        [SerializeField] private EventDispatcher _saveEventDispatcher;

        [SerializeField] private ScriptableIntValue _currentScore;

        [SerializeField] private int _listLimit;

        private static List<SaveData> _saveDatas;

        public static List<SaveData> SaveDatas => _saveDatas;

        private const string RECORDS_KEY = "record";

        private string _filePath;

        private static int _currentRecordPos;

        public static int CurrentRecordPos => _currentRecordPos;

        private void Awake() {
            _saveDatas = new List<SaveData>();
            _filePath = Path.Combine(Application.persistentDataPath, "data.txt");
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
        }

        private void OnCarCollision() {
            var NewRecord = new SaveData {
                date = DateTime.Now.ToString("MM/dd/yyyy HH:mm"),
                score = _currentScore.value.ToString()
            };

            InsertNewRecord(NewRecord);
            CheckTail();
            if (_saveType == SaveType.PlayerPrefs) {
                SaveToPlayerPrefs();
            }
            else {
                SaveToFile();
            }

            _saveEventDispatcher.Dispatch();
        }

        private void InsertNewRecord(SaveData NewRecord) {
            for (int i = _saveDatas.Count - 1; i >= 0; i--) {
                if (Int32.Parse(_saveDatas[i].score) >= Int32.Parse(NewRecord.score)) {
                    _saveDatas.Insert(i + 1, NewRecord);
                    _currentRecordPos = i + 2;
                    return;
                }
            }

            _saveDatas.Insert(0, NewRecord);
            _currentRecordPos = 1;
        }

        private void CheckTail() {
            while (_saveDatas.Count > _listLimit) {
                _saveDatas.Remove(_saveDatas[_saveDatas.Count - 1]);
            }
        }

        private SavedDataWrapper GetWrapper() {
            return (new SavedDataWrapper {
                savedDatas = _saveDatas
            });
        }

        private void LoadFromPlayerPrefs() {
            if (!PlayerPrefs.HasKey(RECORDS_KEY)) {
                return;
            }

            var wrapper = JsonUtility.FromJson<SavedDataWrapper>(PlayerPrefs.GetString(RECORDS_KEY));
            _saveDatas = wrapper.savedDatas;
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
            using (FileStream fileStream = File.Open(_filePath, FileMode.OpenOrCreate)) {
                var wrapper = (SavedDataWrapper) binaryFormatter.Deserialize(fileStream);
                _saveDatas = wrapper.savedDatas;
            }
        }

        private void SaveToFile() {
            var wrapper = GetWrapper();

            var binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = File.Open(_filePath, FileMode.OpenOrCreate)) {
                binaryFormatter.Serialize(fileStream, wrapper);
            }
        }

        private bool IsSaveTypePlayerPrefs() {
            return _saveType == SaveType.PlayerPrefs;
        }
        
        private bool IsSaveTypeFile() {
            return _saveType == SaveType.File;
        }
    }
}