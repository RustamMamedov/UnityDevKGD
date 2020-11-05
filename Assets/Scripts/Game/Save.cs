using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Collections.Generic;
using UnityEngine;
using Events;

namespace Game
{
    public class Save : MonoBehaviour
    {
        [Serializable]
        public class SaveData
        {
            public string date;
            public string score;
        }
        [Serializable]
        private class SavedDataWrapper
        {
            public List<SaveData> saveDatas;
        }
        private enum SaveType
        {
            PlayerPrefs,
            File
        }
        [SerializeField]
        private EventListener _carCollisionEventListener;

        [SerializeField]
        private ScriptableIntValue _currentScore;

        [SerializeField]
        private SaveType _saveType;

        private static List<SaveData> _saveDatas;
        public static List<SaveData> SavedDatas => _saveDatas;
        private const string RECORDS_KEY = "records";
        private string _filePath;
        private void Awake()
        {
            _saveDatas = new List<SaveData>();
            if (_saveType == SaveType.PlayerPrefs)
            {
                LoadFromPlayerPrefs();
            }
            else
            {
                _filePath = Path.Combine(Application.persistentDataPath, "data.txt");
                LoadFromFile();
            }
        }
        private void OnEnable()
        {
            _carCollisionEventListener.OnEventHappened += OnCarCollision;
        }
        private void OnDisable()
        {
            _carCollisionEventListener.OnEventHappened -= OnCarCollision;
        }
        private void OnCarCollision()
        {
            var newRecord = new SaveData
            {
                date = DateTime.Now.ToString("MM/dd/yyyy HH:mm"),
                score=_currentScore.value.ToString()
            };
            _saveDatas.Add(newRecord);
            if (_saveType == SaveType.PlayerPrefs)
            {
                SaveToPlayerPrefs();
            }
            else
            {
                SaveToFile();
            }
        }
        private void LoadFromPlayerPrefs()
        {
            if (!PlayerPrefs.HasKey(RECORDS_KEY))
            {
                return;
            }
            var wrapper = JsonUtility.FromJson<SavedDataWrapper>(PlayerPrefs.GetString(RECORDS_KEY));
            _saveDatas = wrapper.saveDatas;
        }
        private SavedDataWrapper GetWrapper()
        {
            var wrapper = new SavedDataWrapper
            {
                saveDatas = _saveDatas
            };
            return wrapper;
        }
        private void SaveToPlayerPrefs()
        {
            var wrapper = GetWrapper();
            var json = JsonUtility.ToJson(wrapper);
            PlayerPrefs.SetString(RECORDS_KEY, json);
        }
        private void SaveToFile()
        {
            var wrapper = GetWrapper();
            var binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = File.Open(_filePath, FileMode.Open))
            {
                binaryFormatter.Serialize(fileStream, wrapper);
            }
        }
        private void LoadFromFile()
        {
            if (!File.Exists(_filePath))
            {
                return;
            }
            var binaryFormatter = new BinaryFormatter();
            using(FileStream fileStream = File.Open(_filePath, FileMode.OpenOrCreate))
            {
                var wrapper = (SavedDataWrapper)binaryFormatter.Deserialize(fileStream);
                _saveDatas = wrapper.saveDatas;
            }
        }
    }
}
