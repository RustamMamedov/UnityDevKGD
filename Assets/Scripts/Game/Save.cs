using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Sirenix.OdinInspector;

namespace Game {

    public class Save : MonoBehaviour {

        [Serializable]
        public class SaveData{
            public string date;
            public string score;
        }

        [Serializable]
        public class SavedDataWrapper{
            public List<SaveData> saveDatas;
        }

        private enum SaveType{
            PlayerPrefs,
            File

        }

        [SerializeField]
        private EventListener _carCollisionEventListener;

        [SerializeField]
        private EventDispatcher _resultsSavedEventDispatcher;

        [SerializeField]
        private ScriptableIntValue _currentScore;

        [SerializeField]
        private ScriptableIntValue _maxSaves;

        [SerializeField]
        [InfoBox("PlayerPrefs", "IsUsingPlayerPrefs")]
        [InfoBox("/Users/ASUS/AppData/LocalLow/DefaultCompany/UnityDev2020/data.txt", "IsUsingFile")]
        private SaveType _saveType;

        private static List<SaveData> _savedDatas;
        public static List<SaveData> SavedDatas => _savedDatas;

        private static int _currentPlace;
        public static int CurrentPlace => _currentPlace ;

        private const string RECORDS_KEY = "records";
        private string _filePath;

        private void Awake() {
            _savedDatas = new List<SaveData>{};
            _filePath = Path.Combine(Application.persistentDataPath, "data.txt");

            if (_saveType == SaveType.PlayerPrefs) {
                LoadFromPlayerPrefs();
            } else {
                LoadFromFile();
            }

            _savedDatas.Sort((x, y) => x.score.CompareTo(y.score));
        }

        private void OnEnable() {
            _carCollisionEventListener.OnEventHappened += OnCarCollision;
        }

        private void OnDisable() {
            _carCollisionEventListener.OnEventHappened -= OnCarCollision;
        }

        private void SaveToPlayerPrefs() {
            var wrapper = GetWrapper();
            var json = JsonUtility.ToJson(wrapper);
            PlayerPrefs.SetString(RECORDS_KEY, json);
        }
        
        private void SaveToFile() {
            var wrapper = GetWrapper();
            var binaryFormatter = new BinaryFormatter();
            using(FileStream fileStream = File.Open(_filePath, FileMode.OpenOrCreate)) {
                binaryFormatter.Serialize(fileStream, wrapper);
            }
        }

        private void LoadFromPlayerPrefs() {
            if (!PlayerPrefs.HasKey(RECORDS_KEY)) {
                return;
            }

            var wrapper = JsonUtility.FromJson<SavedDataWrapper>(PlayerPrefs.GetString(RECORDS_KEY));
            _savedDatas = wrapper.saveDatas;
        }

        private void LoadFromFile() {
            if (!File.Exists(_filePath)) {
                return;
            }

            var binaryFormatter = new BinaryFormatter();
            using (FileStream filestream = File.Open(_filePath, FileMode.OpenOrCreate)) {
                var wrapper = (SavedDataWrapper) binaryFormatter.Deserialize(filestream);
                _savedDatas = wrapper.saveDatas;
            }
        }

        private void OnCarCollision() {
            int place = -1;

            if (_savedDatas.Count < _maxSaves.value) {
                place = _savedDatas.Count;
            }

            for (int i = _savedDatas.Count - 1; i >= 0; i--) {
                if (Int32.Parse(_savedDatas[i].score) <= _currentScore.value) {
                    place = i + 1;
                    break;
                }
            }

            if ((place != -1) || (_savedDatas.Count < _maxSaves.value)) {
                _savedDatas.Insert(place, CreateNewRecord());
                if (_savedDatas.Count > _maxSaves.value) {
                    _savedDatas.RemoveAt(0);
                    place--;
                }

                _currentPlace = place;
                
                if (_saveType == SaveType.PlayerPrefs) {
                    SaveToPlayerPrefs();
                } else {
                    SaveToFile();
                }

            } else {
                _currentPlace = -1;
            }
            _resultsSavedEventDispatcher.Dispatch();
        }

        private SavedDataWrapper GetWrapper() {
            var wrapper = new SavedDataWrapper {
                saveDatas = _savedDatas
            };
            return wrapper;
        }

        private SaveData CreateNewRecord() {
            return  new SaveData {
                    date = DateTime.Now.ToString("MM/dd/yyy HH:mm"),
                    score = _currentScore.value.ToString()
            };
        }

        private bool IsUsingPlayerPrefs() {
            return (_saveType == SaveType.PlayerPrefs);
        }

        private bool IsUsingFile() {
            return (_saveType == SaveType.File);
        }
    }
}