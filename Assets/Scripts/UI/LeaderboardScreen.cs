using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    
    public class LeaderboardScreen : MonoBehaviour {

        [SerializeField] 
        private Button _menuButton;

        [SerializeField] 
        private GameObject _recordViewPrefab;

        [SerializeField] 
        private GameObject _recordsHolder;

        private List<GameObject> _recordsViewList;

        void Awake() {
            _menuButton.onClick.AddListener(OnMenuButtonClick);
        }

        private void OnEnable() {
            HandleRecordsData();
        }

        private void OnDisable() {
            DestroyRecordsView();
        }

        private void DestroyRecordsView() {
            for (int i = _recordsViewList.Count - 1; i > -1; --i) {
                Destroy(_recordsViewList[i]);
            }
            
            _recordsViewList.Clear();
        }

        private void HandleRecordsData () {
            _recordsViewList = new List<GameObject>();
            
            var records = Save.SaveDatas;

            if (records.Count == 0) {
                return;
            }

            for (int i = 0; i < records.Count; ++i) {
                GenerateRecordView(i + 1, records[i]);
            }
        }

        private void GenerateRecordView(int position, Save.Record record) {
            var recordViewObject = Instantiate(_recordViewPrefab, _recordsHolder.transform);
            recordViewObject.transform.SetParent(_recordsHolder.transform);
            
            var recordView = recordViewObject.GetComponent<RecordView>();
            recordView.SetData(position, record.date, record.Score);
            
            _recordsViewList.Add(recordViewObject);
        }

        private void OnDestroy() {
            _menuButton.onClick.RemoveListener(OnMenuButtonClick);
        }

        private void OnMenuButtonClick() {
            UIManager.Instance.LoadMenu();
        }
    }
}
