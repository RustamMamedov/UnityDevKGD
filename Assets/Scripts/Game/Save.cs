using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Sirenix.OdinInspector;

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

        
        [InfoBox("PlayerPrefs","TypeSave")]
        [InfoBox("@Path.Combine(UnityEngine.Application.persistentDataPath, \"data.txt\")", "!TypeSave")]
        [SerializeField]
        private SaveType _saveType;

        [SerializeField]
        private ScriptableIntValue _wasSaved;

        [SerializeField]
        private ScriptableIntValue _indexOfNewRecord;

        private static List<SaveData> _saveDatas;
        public static List<SaveData> SavedDatas => _saveDatas;
        private const string RECORDS_KEY = "records";
        private string _filePath;

       
        private static bool TypeSave(SaveType saveType)
        {
            return saveType == SaveType.PlayerPrefs;
        }
        private void Awake()
        {
            _saveDatas = new List<SaveData>();
            _indexOfNewRecord.value = 10;
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
            _wasSaved.value = 0;
            var newRecord = new SaveData
            {
                date = DateTime.Now.ToString("MM/dd/yyyy HH:mm"),
                score=_currentScore.value.ToString()
            };
            if (_saveDatas.Count == 10)
            {
                SortRecords();
                if (Int32.Parse(newRecord.score) > Int32.Parse(_saveDatas[_saveDatas.Count - 1].score))
                {
                    _saveDatas.RemoveAt(_saveDatas.Count - 1);
                    _saveDatas.Add(newRecord);
                    var score = newRecord.score;
                    SortRecords();
                    for(int i=0;i< _saveDatas.Count; i++)
                    {
                        if (Int32.Parse(score) <= Int32.Parse(_saveDatas[i].score))
                        {
                            _indexOfNewRecord.value = i;
                        }
                    }
                }
            }
            else
            {
                _saveDatas.Add(newRecord);
                var score = newRecord.score;
                SortRecords();
                for (int i = 0; i < _saveDatas.Count; i++)
                {
                    if (Int32.Parse(score) <= Int32.Parse(_saveDatas[i].score))
                    {
                        _indexOfNewRecord.value = i;
                    }
                }
            }
            if (_saveType == SaveType.PlayerPrefs)
            {
                SaveToPlayerPrefs();
            }
            else
            {
                SaveToFile();
            }
        }
        private void SortRecords()
        {
            for(int i = 0; i < _saveDatas.Count-1; i++)
            {
                for (int j = i + 1; j < _saveDatas.Count; j++)
                {
                    if (Int32.Parse(_saveDatas[i].score) < Int32.Parse(_saveDatas[j].score))
                    {
                        var tmpdate = _saveDatas[i].date;
                        _saveDatas[i].date = _saveDatas[j].date;
                        _saveDatas[j].date = tmpdate;
                        var tmp = _saveDatas[i].score;
                        _saveDatas[i].score = _saveDatas[j].score;
                        _saveDatas[j].score = tmp;
                    }
                }
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
            _wasSaved.value = 1;
        }
        private void SaveToFile()
        {
            var wrapper = GetWrapper();
            var binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = File.Open(_filePath, FileMode.Open))
            {
                binaryFormatter.Serialize(fileStream, wrapper);
            }
            _wasSaved.value = 1;
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
