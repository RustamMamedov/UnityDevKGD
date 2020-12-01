using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Sirenix.OdinInspector;

namespace Game {

    public class Save : MonoBehaviour {

        //public static Save Instance;

        [SerializeField]
        private bool _saveParametrsSettings;

        [SerializeField]
        private EventListeners _saveResultEventListener;

        [Serializable]
        public class SaveData {

            public string date;
            public string score;
        }

        public static SaveData currentResult;

        //for Sort
        private class ScoreComparer : IComparer<SaveData> {

            public int Compare(SaveData a1, SaveData a2) {
                var value1 = Int32.Parse(a1.score);
                var value2 = Int32.Parse(a2.score);
                if (value1 > value2) {
                    return -1;
                }
                else if (value1 < value2) {
                    return 1;
                }

                return 0;
            }
        }

        private ScoreComparer comparer = new ScoreComparer();

        [SerializeField]
        private int _countBestResults;

        [Serializable]
        private class SavedDataWrapper {

            public List<SaveData> saveDatas;
        }

        [SerializeField]
        private EventListeners _carCollisionEventListeners;

        [SerializeField]
        private ScriptableIntValue _currentScore;

        private static List<SaveData> _savedData;

        public static List<SaveData> SavedDatas => _savedData;

        private const string Records_Key = "records";

        private string _filePath;

        private enum SaveType {
            PlayerPrefs,
            FromFile
        }
#if UNITY_EDITOR
        [InfoBox("$InfoString", "Information")]
#endif
        [SerializeField]
        private SaveType _saveType;
#if UNITY_EDITOR
        private string InfoString;
        private bool Information(SaveType saveData) {
            if (saveData == SaveType.FromFile) {
                InfoString = Path.Combine(Application.persistentDataPath, "data.txt");
            }
            else {
                InfoString = "PlayerPrefs";
            }
            return true;
        }
#endif

        #region SaveParametrsSettings

        [ShowIfGroup(nameof(_saveParametrsSettings))]
        [BoxGroup(nameof(_saveParametrsSettings)+ "/Sattings")]
        [SerializeField]
        private ScriptableFloatValue _soundValue;

        [BoxGroup(nameof(_saveParametrsSettings) + "/Sattings")]
        [SerializeField]
        private ScriptableIntValue _difficultyValue;

        [BoxGroup(nameof(_saveParametrsSettings) + "/Sattings")]
        [SerializeField]
        private ScriptableIntValue _lightValue;

        [Serializable]
        public class SaveParametrs {

            public string sound;
            public string difficult;
            public string light;
        }

        private static SaveParametrs _savedParametrs;

        private const string Parametrs_Key = "parametrs";

        [SerializeField]
        private class SavedParametrsWrapper { 
            public List<SaveParametrs> saveParametrs;
        }

        #endregion SaveParametrsSettings

        private void Awake() {
            //Instance = this;
            _savedData = new List<SaveData>();
            _savedParametrs = new SaveParametrs();
            //Debug.Log(Application.persistentDataPath);
            _filePath = Path.Combine(Application.persistentDataPath, "data.txt");
            if (!_saveParametrsSettings) {
                if (_saveType == SaveType.PlayerPrefs) {
                    LoadFromPlayerPrefs();
                }
                else {
                    LoadFromFile();
                }
            }
            else {
                LoadParametrs();
            }
        }

        private void OnEnable() {
            //_carCollisionEventListeners.OnEventHappened += OnCarCollison;
            if (!_saveParametrsSettings) {
                _saveResultEventListener.OnEventHappened += SaveFromCollision;
            }
            else {
                _saveResultEventListener.OnEventHappened += SaveParametrsToPlayerPrefs;
            }
            currentResult = null;
        }

        private void OnDisable() {
            //_carCollisionEventListeners.OnEventHappened -= OnCarCollison;
            if (!_saveParametrsSettings) {
                _saveResultEventListener.OnEventHappened -= SaveFromCollision;
            }
            else {
                _saveResultEventListener.OnEventHappened -= SaveParametrsToPlayerPrefs;
            }
            _savedData.Clear();
        }

        private void SaveFromCollision() {
            OnCarCollison();
        }

        private void OnCarCollison() {
            var newRecord = new SaveData {
                date = DateTime.Now.ToString("MM/dd/yyyy HH:mm"),
                score = _currentScore.Value.ToString()

            };
            //Debug.Log($"new record:{newRecord.date} {newRecord.score}");
            currentResult = newRecord;
            _savedData.Add(newRecord);
            if (_saveType == SaveType.PlayerPrefs) {
                SaveFromPlayerPrefs();
            }
            else {
                SaveFromFile();
            }
            //Debug.Log("SaveData");
            //Debug.Log($"{_savedData[0].score}   {_savedData.Count}");
            Sort10BestResult();
            //Debug.Log($"{_savedData[0].score}   {_savedData.Count}");
        }

        private void Sort10BestResult() {
            _savedData.Sort(comparer);
            var count = _savedData.Count;
            for (int i = count-1; i >= _countBestResults; i--) {
                _savedData.RemoveAt(i);
            }
        }

        private void LoadFromPlayerPrefs() {
            if (!PlayerPrefs.HasKey(Records_Key)) {
                return;
            }
            var wrapper = JsonUtility.FromJson<SavedDataWrapper>(PlayerPrefs.GetString(Records_Key));
            _savedData = wrapper.saveDatas;

        }

        private SavedDataWrapper GetDataWrapper() {
            var wrapper = new SavedDataWrapper {
                saveDatas = _savedData
            };
            return wrapper;
        }

        private void SaveFromPlayerPrefs() {
            var wrapper = GetDataWrapper();
            var json = JsonUtility.ToJson(wrapper);
            //Debug.Log(json);
            PlayerPrefs.SetString(Records_Key, json);
        }

        private void SaveFromFile() {
            var wrapper = GetDataWrapper();

            var binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = File.Open(_filePath, FileMode.OpenOrCreate)) {
                binaryFormatter.Serialize(fileStream,wrapper);
            }
        }

        private void LoadFromFile() {
            if (!File.Exists(_filePath)) {
                return;
            }

            var binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream=File.Open(_filePath,FileMode.OpenOrCreate)) {
                var wrapper = (SavedDataWrapper) binaryFormatter.Deserialize(fileStream);
                _savedData = wrapper.saveDatas;
            }
            //Debug.Log(_savedData.Count);
        }

        private SavedParametrsWrapper GetWrapperParametrs() {
            var list = new List<SaveParametrs>();
            list.Add(_savedParametrs);
            var wrapper = new SavedParametrsWrapper {
                saveParametrs=list
            };
            return wrapper;
        }

        private void SaveParametrsToPlayerPrefs() {
            _savedParametrs.difficult = $"{_difficultyValue.Value}";
            _savedParametrs.sound = $"{_soundValue.Value}";
            _savedParametrs.light = $"{_lightValue.Value}";
            var wrapper = GetWrapperParametrs();
            var json = JsonUtility.ToJson(wrapper);
            PlayerPrefs.SetString(Parametrs_Key, json);
        }

        private void LoadParametrs() {
            if (!PlayerPrefs.HasKey(Parametrs_Key)) {
                return;
            }
            var wrapper = JsonUtility.FromJson<SavedParametrsWrapper>(PlayerPrefs.GetString(Parametrs_Key));
            _savedParametrs = wrapper.saveParametrs[wrapper.saveParametrs.Count - 1];
            _difficultyValue.Value = Int32.Parse(_savedParametrs.difficult);
            _soundValue.Value = float.Parse(_savedParametrs.sound);
            _lightValue.Value = Int32.Parse (_savedParametrs.light);
        }
    }
}
