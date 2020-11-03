using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Events;
using UnityEngine;

namespace Game {

    public class Save : MonoBehaviour {

        public class  SaveData {

            public string date;
            public string score;
        }
        
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
        private SaveType _saveType;
        private static List<SaveData> _savedDatas;
        public static List<SaveData> SavedDatas => _savedDatas;
        private const string RECORDS_KEY = "records";
        private string _filePath;
        
        private void Awake() {
            _savedDatas = new List<SaveData>();
            _filePath = Path.Combine(Application.persistentDataPath, "data.txt");
            if (_saveType == SaveType.PlayerPrefs) {
                SaveToPlayerPrefs();
            } else {
                LoadFromFile();
            }
        }
        
        private void OnEnable() {
            _carCollisionEventListener.OnEventHappened += OnCarCollision;
        }
        
        private void OnDisable() {
            _carCollisionEventListener.OnEventHappened += OnCarCollision;
        }

        private void OnCarCollision() {
            var newRecord = new SaveData() {
                date = DateTime.Now.ToString("MM/dd/yyyy HH:mm"),
                score = _currentScore.value.ToString()
            };
            _savedDatas.Add(newRecord);

            SaveToFile();
            // SaveToPlayerPrefs();
        }

        private void LoadFromPlayerPrefs() {
            if (!PlayerPrefs.HasKey(RECORDS_KEY)) {
                return;
            }

            var wrapper = JsonUtility.FromJson<SavedDataWrapper>(PlayerPrefs.GetString(RECORDS_KEY));
            _savedDatas = wrapper.saveDatas;
        }

        private SavedDataWrapper GetWrapper() {
            var wrapper = new SavedDataWrapper() {
                saveDatas = _savedDatas
            };
            return wrapper;
        }
        private void SaveToPlayerPrefs() {
            var wrapper = GetWrapper();
            var json = JsonUtility.ToJson(_savedDatas);
            PlayerPrefs.SetString(RECORDS_KEY, json);
        }

        private void LoadFromFile() {
            if (!File.Exists(_filePath)) {
                return;
            }
            
            var binaryFormatter = new BinaryFormatter();
            using(FileStream fileStream = File.Open(_filePath, FileMode.Open)) { 
                var wrapper = (SavedDataWrapper) binaryFormatter.Deserialize(fileStream);
                _savedDatas = wrapper.saveDatas;
            }
        }

        private void SaveToFile() {
            var wrapper = GetWrapper();
            var binaryFormatter = new BinaryFormatter();
            using(FileStream fileStream = File.Open(_filePath, FileMode.OpenOrCreate)) { 
                binaryFormatter.Serialize(fileStream, wrapper);
            }
        }
    }
}