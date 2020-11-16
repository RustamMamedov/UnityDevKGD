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
        private ScriptableIntValue _playerScorePosition;

        [InfoBox("$_typeInfo")]
        [SerializeField]
        private SaveType _saveType;
        private static string _typeInfo = "PlayerPrefs";

        [SerializeField]
        private EventDispatcher _scoreSavedEventDispather;

        private static List<SaveData> _saveDatas;
        public static List<SaveData> SavedDatas => _saveDatas;

        private const string RECORDS_KEY = "records";
        private string _filePath;


        private void OnDrawGizmosSelected() {
            if (_saveType == SaveType.PlayerPrefs) {
                _typeInfo = "PlayerPrefs";
            }
            else {
                _filePath = Path.Combine(Application.persistentDataPath, "data.txt");
                _typeInfo = _filePath;
            }
        }




        private void Awake() {
            _saveDatas = new List<SaveData>();
            _filePath = Path.Combine(Application.persistentDataPath, "data.txt");
            Debug.Log(_filePath);
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

            if (_saveDatas.Count > 1) {
                DataSorting(_saveDatas);
            }
            if (_saveType == SaveType.PlayerPrefs) {
                SaveToPlayerPrefs();
            }
            else {
                SaveToFile();
            }

            _scoreSavedEventDispather.Dispatch();
        }

        private void DataSorting(List<SaveData> saveDatas) {
            for (int i = saveDatas.Count - 2; i >= 0; i--) {

                if (int.Parse(saveDatas[i].score) > int.Parse(saveDatas[saveDatas.Count - 1].score)) {
                    saveDatas.Insert(i + 1, saveDatas[saveDatas.Count - 1]);
                    saveDatas.RemoveAt(saveDatas.Count - 1);
                    _playerScorePosition.value = i + 1;
                    break;
                }
                if (i == 0) {
                    _playerScorePosition.value = 0;
                    saveDatas.Insert(i, saveDatas[saveDatas.Count - 1]);
                    saveDatas.RemoveAt(saveDatas.Count - 1);
                }

            }
            if (saveDatas.Count > 10) {
                saveDatas.RemoveRange(10, saveDatas.Count - 10);
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