using UnityEngine;
using Events;
using System;
using System.Collections.Generic;
using TMPro;

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
        private ScriptableIntValue _currentScore;

        private List<SaveData> _saveDatas;
        public List<SaveData> savedDatas => _saveDatas;

        private const string RECORDS_KAY = "records";

        private void Awake() {
            _saveDatas = new List<SaveData>();
            LoadFromPlayerPrefs();
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
            Debug.Log($"new record: {newRecord.date} {newRecord.score}");
            _saveDatas.Add(newRecord);

            SaveDateToPlayerPrefs();
        }
        
        private void LoadFromPlayerPrefs() {
            if (!PlayerPrefs.HasKey(RECORDS_KAY)) {
                return;
            }

            var wrapper = JsonUtility.FromJson<SavedDataWrapper>(PlayerPrefs.GetString(RECORDS_KAY));
            _saveDatas = wrapper.saveDatas;
            //Debug.Log(_saveDatas.Count);
        }

        private void SaveDateToPlayerPrefs() {
            var wrapper = new SavedDataWrapper {
                saveDatas = _saveDatas
            };
            var json = JsonUtility.ToJson(wrapper);
            PlayerPrefs.SetString(RECORDS_KAY, json);
        }
    }

    
}

