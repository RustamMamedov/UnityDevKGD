using System;
using System.Collections.Generic;
using Events;
using UnityEngine;

namespace Game {

    public class Save : MonoBehaviour {

        [Serializable]
        public class SaveData {

            public string date;
            public string score;
        }

        [SerializeField] private EventListener _carCollisionEventListener;

        [SerializeField] private ScriptableIntValue _currentScore;

        private List<SaveData> _saveData;
        private const string RECORDS_KEY = "records";

        private void Awake() {
            _saveData = new List<SaveData>();
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
                score = _currentScore.value.ToString(),
            };
            Debug.Log($"new record {newRecord.date} {newRecord.score} ");
            _saveData.Add(newRecord);
            SaveDataToPlayerPrefs();
        }

        private void SaveDataToPlayerPrefs() {
            
        }
    }
}