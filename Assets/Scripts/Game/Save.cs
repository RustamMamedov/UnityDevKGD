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

        //туть пропустил
        [SerializeField]
        private class SavedDataWrapper {

            public List<SaveData> saveDatas;
        }

        [SerializeField]
        private EventListener _carCollisionEventListener;

        [SerializeField]
        private ScriptableIntValue _currentScore;

        private List<SaveData> _savedDatas;
        public List<SaveData> SavedDatas => _savedDatas;


        private const string RECOREDS_KEY = "records";

        private void Awake() {
            _savedDatas = new List<SaveData>();
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
            Debug.Log($"new record {newRecord.date} {newRecord.score}");
            _savedDatas.Add(newRecord);

            SaveToPlayerPrefs();
        }

        private void LoadFromPlayerPrefs() {
            if (PlayerPrefs.HasKey(RECOREDS_KEY))
                return;

            var wrapper = JsonUtility.FromJson<SavedDataWrapper>(PlayerPrefs.GetString(RECOREDS_KEY));
            _savedDatas = wrapper.saveDatas;
        }

        private void SaveToPlayerPrefs() {
            var wrapper = new SavedDataWrapper {
                saveDatas = _savedDatas
            };
            var json = JsonUtility.ToJson(wrapper);
            PlayerPrefs.SetString(RECOREDS_KEY, json);
        }

    }
}