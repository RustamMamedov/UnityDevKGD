using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using System;

namespace Game {

    public class Save : MonoBehaviour {

        public class SaveData {

            public string date;
            public string score;
        }

        [SerializeField]
        private EventListener _carCollisionEventListener;

        [SerializeField]
        private ScriptableIntValue _currentScore;

        private List<SaveData> _savedData;

        private void Awake() {
            _savedData = new List<SaveData>();
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
            Debug.Log($"new record {newRecord.date} {newRecord.score}");
            _savedData.Add(newRecord);
        }

    }
}