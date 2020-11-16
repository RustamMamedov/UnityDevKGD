using System;
using System.Collections.Generic;
using Events;
using UnityEngine;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Sirenix.OdinInspector;

namespace Game {

    public class Save : MonoBehaviour {

        [Serializable]
        public class SaveData {

            public string date;
            public string score;
            public bool newScore;
        }

        [Serializable]
        private class SavedDataWrapper {

            public List<SaveData> saveDatas;
        }

        [InfoBox("PlayerPrefs", "isSaveTypePlayerPrefs")]
        [InfoBox("/Users/anastasialutovinova/Library/Application Support/DefaultCompany/UnityDev2020/data.txt", "isSaveTypeFile")]
        private enum SaveType {

            PlayerPrefs,
            File,
        }

        [SerializeField]
        private SaveType _saveType;

        [SerializeField]
        private EventListener _carCollisionEventListener;

        [SerializeField]
        private ScriptableIntValue _currentScore;

        private static List<SaveData> _saveDatas;
        public static List<SaveData> SavedDatas => _saveDatas;

        private const string RECORDS_KEY = "records";
        private string _filePath;

        private bool isSaveTypePlayerPrefs() {
            return _saveType == SaveType.PlayerPrefs;
        }

        private bool isSaveTypeFile() {
            return _saveType == SaveType.File;
        }

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
            _carCollisionEventListener.OnEventHappend += OnCarCollision;
        }

        private void OnDisable() {
            _carCollisionEventListener.OnEventHappend -= OnCarCollision;
        }

        private void OnCarCollision() {
            var newRecord = new SaveData {
                date = DateTime.Now.ToString("MM/dd/yyyy HH:mm "),
                score = _currentScore.value.ToString(),
                newScore = true,
            };

            AddNewRecord(newRecord);

            if (_saveType == SaveType.PlayerPrefs) {
                SaveToPlayerPrefs();
            } else {
                SaveToFile();
            }
        }
       
        private void AddNewRecord(SaveData newRecord) {
            foreach (SaveData save in _saveDatas) {
                save.newScore = false;
            }

            _saveDatas.Add(newRecord);
            _saveDatas = _saveDatas.OrderByDescending(save => Int32.Parse(save.score)).ToList<SaveData>();

            if (_saveDatas.Count > 10) {
                _saveDatas.RemoveAt(_saveDatas.Count - 1);
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
            using (FileStream fileStream = File.Open(_filePath, FileMode.Open)) {
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