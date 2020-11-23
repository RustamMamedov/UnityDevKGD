using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game;

namespace UI {
    public class LeaderboardScreen : MonoBehaviour {

        [SerializeField]
        private Button _menuButton;
        [SerializeField]
        private GameObject _bestRideRecordPrefab;

        [SerializeField]
        private GameObject _recordsGameObject;

        [SerializeField]
        private ScriptableIntValue _playerScorePosition;

        [SerializeField]
        private List<GameObject> _bestRideRecord;

        private void Awake() {
            _menuButton.onClick.AddListener(OnMenuButtonClick);
        }

        private void OnEnable() {
            AddBestRideRecordPrefab();
        }

        private void OnDisable() {
            if (_bestRideRecord != null) {
                for (int i = 0; i < _bestRideRecord.Count; i++) {
                    Destroy(_bestRideRecord[i].gameObject);
                }
                _bestRideRecord.Clear();
            }
        }

        private void AddBestRideRecordPrefab() {
            if (_bestRideRecord == null) {
                _bestRideRecord = new List<GameObject>();
            }

            var recordsList = Save.SavedDatas;
            for (int i = 0; i < recordsList.Count; i++) {

                _bestRideRecord.Add(Instantiate(_bestRideRecordPrefab, _recordsGameObject.transform));

                var recordView = _bestRideRecord[i].GetComponent<RecordView>();
                recordView.SetData(i + 1, recordsList[i].date, recordsList[i].score);

                if (_playerScorePosition.value == i) {
                    recordView.ShowFlower();
                }
            }

        }

        private void OnMenuButtonClick() {

            UIManager.Instance.LoadMenu();
        }


    }
}

