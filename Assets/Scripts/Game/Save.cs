using System.Collections.Generic;
using UnityEngine;
using System;
using Events;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Sirenix.OdinInspector;
using System.Linq;

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

        [SerializeField]
        private EventListener _carCollisionEventListener;

        [SerializeField]
        private EventDispatcher _gameSavedEventDispatcher;

        [SerializeField]
        private ScriptableIntValue _currentScore;

        #region Save Type

        private enum SaveType {
            PlayerPrefs,
            File
        }

        [SerializeField]
        [InfoBox("@GetMessageForSaveType()", InfoMessageType.Info)]
        private SaveType _saveType;

        private string GetMessageForSaveType() {
            if (_saveType == SaveType.File) {
                return String.Concat(UnityEngine.Application.persistentDataPath, Path.AltDirectorySeparatorChar, "data.txt");
            }

            return "PlayerPrefs";
        }

        #endregion Save Type

        [SerializeField]
        private int _numberRecordsInTable;

        [SerializeField]
        private ScriptableIntValue _lastRecordIndex;

        private static List<SaveData> _saveDatas;
        public static List<SaveData> SavedDatas => _saveDatas;

        private const string RECORDS_KEY = "records";
        private string _filePath;

        private void Awake() {
            _lastRecordIndex.value = -1;
            _saveDatas = new List<SaveData>();
            _filePath = Path.Combine(Application.persistentDataPath, "data.txt");
            if (_saveType == SaveType.PlayerPrefs) {
                LoadFromPlayerPrefs();
            } else {
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
            if (CurrentScoreIsNewRecord()) {
                AddCurrentScoreToTable();
                if (_saveType == SaveType.PlayerPrefs) {
                    SaveToPlayerPrefs();
                } else {
                    SaveToFile();
                }
            }
            _gameSavedEventDispatcher.Dispatch();
        }

        private bool CurrentScoreIsNewRecord() {
            if (_saveDatas.Any(res => Int32.Parse(res.score) == _currentScore.value)) {
                return false;
            }
            if (_saveDatas.Count < _numberRecordsInTable) {
                return true;
            }
            var lastScoreInTop = Int32.Parse(_saveDatas[_saveDatas.Count - 1].score);
            return _currentScore.value > lastScoreInTop;
        }

        private void AddCurrentScoreToTable() {
            var newRecord = new SaveData {
                date = DateTime.Now.ToString("MM/dd/yyyy HH:mm"),
                score = _currentScore.value.ToString()
            };

            _saveDatas.Add(newRecord);
            _saveDatas.Sort((res1, res2) => (Int32.Parse(res2.score)).CompareTo(Int32.Parse(res1.score)));
            _lastRecordIndex.value = _saveDatas.IndexOf(newRecord);

            if (_saveDatas.Count > _numberRecordsInTable)
                _saveDatas.RemoveAt(_saveDatas.Count - 1);
        }

        private SavedDataWrapper GetWrapper() {
            var wrapper = new SavedDataWrapper {
                saveDatas = _saveDatas
            };
            return wrapper;
        }

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