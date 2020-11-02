using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

namespace Game {

    public class Save : MonoBehaviour {

        //Данный метод подходит только для хранения временных файлов

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
        private EventListeners _carCollisionEventListeners;

        [SerializeField]
        private ScriptableIntValue _currentScore;

        private List<SaveData> _savedData= new List<SaveData>();

        public List<SaveData> SavedDatas => _savedData;

        private const string Records_Key="records";

        private void OnEnable() {
            _carCollisionEventListeners.OnEventHappened += OnCarCollison;
        }

        private void OnDisable() {
            _carCollisionEventListeners.OnEventHappened -= OnCarCollison;
        }

        private void OnCarCollison() {
            var newRecord = new SaveData {
                date = DateTime.Now.ToString("MM/dd/yyyy HH:mm"),
                score = _currentScore.Value.ToString()
            };
            Debug.Log($"new record:{newRecord.date} {newRecord.score}");
            _savedData.Add(newRecord);
        }

        private void LoadFromPleyerPrefs() {
            if (!PlayerPrefs.HasKey(Records_Key)) {
                return;
            }

            var wrapper = JsonUtility.FromJson<SavedDataWrapper>(PlayerPrefs.GetString(Records_Key));
            _savedData = wrapper.saveDatas;
        }

        private void SaveDataToPrefs() {
            var wrapper = new  SavedDataWrapper{
                saveDatas = _savedData
            };
            var json = JsonUtility.ToJson(wrapper);
            PlayerPrefs.SetString(Records_Key,json);
        }
    }
}
