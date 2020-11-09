﻿using System;
using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class LeaderboardScreen : MonoBehaviour {

        [SerializeField]
        private Button _menuButton;

        [SerializeField]
        private RecordView _recordViewPrefab;

        [SerializeField]
        private Transform _recordViewParent;

        private RecordView[] _recordViews;

        private void Awake() {
            _menuButton.onClick.AddListener(OnMenuButtonClick);
        }

        private void OnMenuButtonClick() {
            UIManager.Instance.LoadMenu();
        }

        private int CompareRecords(Save.SaveData sd1, Save.SaveData sd2) {
            var score1 = Int32.Parse(sd1.score);
            var score2 = Int32.Parse(sd2.score);
            return score1<score2?1 : score1> score2? - 1 : 0;
        }

        private void OnEnable() {
            Save.SavedDatas.Sort(CompareRecords);
            _recordViews = new RecordView[Save.SavedDatas.Count];
            for (int i = 0; i < _recordViews.Length; i++) {
                var recordView = Instantiate(_recordViewPrefab, _recordViewParent);
                recordView.SetData(i + 1, Save.SavedDatas[i].date, Save.SavedDatas[i].score);
                _recordViews[i] = recordView;
            }
        }

        private void OnDestroy() {
            for (int i = 0; i < _recordViews.Length; i++) {
                Destroy(_recordViews[i]);
            }
            _recordViews = null;
        }
    }
}