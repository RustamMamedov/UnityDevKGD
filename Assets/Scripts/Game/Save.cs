using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Events;
using UI;
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
            File
        }

        [SerializeField]
        private EventListener _carCollisionEventListener;

        [SerializeField]
        private ScriptableIntValue _currentScore;

        [SerializeField]
        [InfoBox("PlayerPrefs", nameof(IsSaveToPrefs))] 
        [InfoBox("@Path.Combine(UnityEngine.Application.persistentDataPath, \"data.txt\")", nameof(IsSaveToFile))]
        private SaveType _saveType;

        private static List<SaveData> _saveDatas;
        public static List<SaveData> SavedDatas => _saveDatas;

        private const string RECORDS_KEY = "rercords";
        private string _filePath;

        private bool _isSaveDone;   

        public static int NewPlace;

        private void Awake() {
            _saveDatas = new List<SaveData>();
            _filePath = Path.Combine(Application.persistentDataPath, "data.txt");

            if (_saveType == SaveType.PlayerPrefs) {
                LoadFromPlayerPrefs();
            } else {
                LoadFormFile();
            }
        }

        private void OnEnable() {
            _carCollisionEventListener.OnEventHappened += OnCarCollision;
        }

        private void OnDisable() {
            _carCollisionEventListener.OnEventHappened -= OnCarCollision;
        }

        private void OnCarCollision() {

            _isSaveDone = false;
            SortSave();
            NewPlace = -1;

            var newRecord = new SaveData {
                date = DateTime.Now.ToString("MM/dd/yyyy HH:mm"),
                score = _currentScore.value.ToString()
            };

            Debug.Log($"new record: {newRecord.date} {newRecord.score}");

            if (IsSaveFull()) {
                for (int i = 0; i < _saveDatas.Count; i++) {
                    if (GetScore(_saveDatas[i].score) < GetScore(newRecord.score)) {
                        _saveDatas.RemoveAt(_saveDatas.Count - 1);
                        _saveDatas.Insert(i, newRecord);
                        NewPlace = i;
                        break;
                    }
                }
            } else {
                _saveDatas.Add(newRecord);
            }

            if (_saveType == SaveType.PlayerPrefs) {
                SaveToPlayerPrefs();
            } else {
                SaveToFile();
            }

            if (_isSaveDone) {
                UIManager.Instance.ShowLeaderboardsScreen();
            }
        }

        private bool IsSaveToPrefs() { 
            return _saveType == SaveType.PlayerPrefs; 
        } 
 
        private bool IsSaveToFile() { 
            return _saveType == SaveType.File; 
        } 

        private int GetScore(string score) {
            return Int32.Parse(score);
        }

        private void SortSave() {
            for (int i = 0; i < _saveDatas.Count - 1; i++) {
                for (int j = i + 1; j < _saveDatas.Count; j++) {
                    if (GetScore(_saveDatas[j].score) > GetScore(_saveDatas[i].score)) {
                        string t = _saveDatas[j].score;
                        _saveDatas[j].score = _saveDatas[i].score;
                        _saveDatas[i].score = t;
                    }
                }
            }
        }

        private bool IsSaveFull() {
            if (_saveDatas.Count == 10) {
                return true;
            } else {
                return false;
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

            var json = JsonUtility.ToJson(GetWrapper());
            PlayerPrefs.SetString(RECORDS_KEY, json);

            _isSaveDone = true;
        }

        private void LoadFormFile() {

            if (!File.Exists(_filePath)) {
                return;
            }

            var binaryFormatter = new BinaryFormatter();
            using(FileStream fileStream = File.Open(_filePath, FileMode.OpenOrCreate)) {
                var wrapper = (SavedDataWrapper) binaryFormatter.Deserialize(fileStream);
                _saveDatas = wrapper.saveDatas;
            }
        }

        private void SaveToFile() {

            var binaryFormatter = new BinaryFormatter();
            using(FileStream fileStream = File.Open(_filePath, FileMode.OpenOrCreate)) {
                binaryFormatter.Serialize(fileStream, GetWrapper());
            }

            _isSaveDone = true;
        }
    }
}