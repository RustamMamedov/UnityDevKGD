using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Game {

    public class Save : MonoBehaviour {

        public class SaveData {

            public string date;
            public string score;
        }

        //туть пропустил
        [Serializable]
        private class SavedDataWrapper {

            public List<SaveData> saveDatas;
        };

        private enum SaveType {

            PlayerPrefs,
            File
        }

        [SerializeField]
        private SaveType _savedType;

        [SerializeField]
        private EventListener _carCollisionEventListener;

        [SerializeField]
        private ScriptableIntValue _currentScore;

        private static List<SaveData> _savedDatas;
        public static List<SaveData> SavedDatas => _savedDatas;


        private const string RECOREDS_KEY = "records";
        private string _filePath;
        #region Enable/Disable
        private void OnEnable() {
            _carCollisionEventListener.OnEventHappened += OnCarCollision;
        }

        private void OnDisable() {
            _carCollisionEventListener.OnEventHappened -= OnCarCollision;
        }
        #endregion Enable/Disable


        private void Awake() {
            _savedDatas = new List<SaveData>();
            _filePath = Path.Combine(Application.persistentDataPath, "data.txt");
            if (_savedType == SaveType.PlayerPrefs)
                LoadFromPlayerPrefs();
            else
                LoadFromFile();
        }



        private void OnCarCollision() {
            var newRecord = new SaveData {
                date = DateTime.Now.ToString("MM/dd/yyyy HH:mm"),
                score = _currentScore.value.ToString()
            };
            Debug.Log($"new record {newRecord.date} {newRecord.score}");
            _savedDatas.Add(newRecord);


            if(_savedType==SaveType.PlayerPrefs)
                SaveToPlayerPrefs();
            else
                SaveToFile();
        }

        private SavedDataWrapper GetWrapper() {
            var wrapper = new SavedDataWrapper {
                saveDatas = _savedDatas
            };
            return wrapper;
        }
        #region PlayerPrefs
        private void SaveToPlayerPrefs() {
            var wrapper = GetWrapper();
            var json = JsonUtility.ToJson(wrapper);
            PlayerPrefs.SetString(RECOREDS_KEY, json);
        }

        private void LoadFromPlayerPrefs() {
            if (!PlayerPrefs.HasKey(RECOREDS_KEY))
                return;

            var wrapper = JsonUtility.FromJson<SavedDataWrapper>(PlayerPrefs.GetString(RECOREDS_KEY));
            _savedDatas = wrapper.saveDatas;
        }
        #endregion PlayerPrefs
        #region File
        private void SaveToFile() {
            var wrapper = GetWrapper();

            var binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = File.Open(_filePath, FileMode.OpenOrCreate)) {
                binaryFormatter.Serialize(fileStream, wrapper);
            }

        }

        private void LoadFromFile() {
            if (!File.Exists(_filePath)) {
                return;
            }
            
            var binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream=File.Open(_filePath,FileMode.Open)) {
                var wrapper = (SavedDataWrapper)binaryFormatter.Deserialize(fileStream);
                _savedDatas = wrapper.saveDatas;
            }
            Debug.Log(_savedDatas.Count);
        }
        #endregion File


    }
}