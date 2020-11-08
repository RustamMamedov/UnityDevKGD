using Events;
using System;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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
        private EventDispatcher _dataSaved;

        private static List<SaveData> _saveDatas;
        public static List<SaveData> SavedDatas => _saveDatas;

        private const string RECORDS_KEY = "records";
        private string _filePath;
        public static int savePosition;

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
            var newRecord = new SaveData {
                date = DateTime.Now.ToString("MM/dd/yyyy HH:mm"),
                score = _currentScore.value.ToString()
            };
            _saveDatas.Add(newRecord);
            СonversionResolt(newRecord);

            if (_saveType == SaveType.PlayerPrefs) {
                SaveToPlayerPrefs();
            }
            else {
                SaveToFile();
            }

            _dataSaved.Dispatch();
        }

        private SavedDataWrapper GetWrapper() {
            var wrapper = new SavedDataWrapper {
                saveDatas = _saveDatas
            };
            return wrapper;
        }

        //сохрание с помощью PlayerPrefs

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

        //сохрание с помощью File

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

        private void СonversionResolt(SaveData currentResult) {
            //сортировка
            for (var i = 0; _saveDatas.Count - 1 > i; i++) {
                for (var j = i + 1; _saveDatas.Count > j; j++) {
                    if (int.Parse(_saveDatas[i].score) < int.Parse(_saveDatas[j].score)) {
                        var temp = _saveDatas[i];
                        _saveDatas[i] = _saveDatas[j];
                        _saveDatas[j] = temp;
                    }
                }
            }

            for (var i = 0; _saveDatas.Count > i; i++) {
                if (_saveDatas[i] == currentResult) {
                    savePosition = i;
                    break;
                }
            }

            //удаление
            for (var i = 10; _saveDatas.Count > i; i++) {
                _saveDatas.RemoveAt(i);
            }
        }
    }
}