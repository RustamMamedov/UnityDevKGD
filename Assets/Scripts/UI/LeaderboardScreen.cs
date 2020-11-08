using UnityEngine.UI;
using UnityEngine;
using Game;
using System.Collections.Generic;

namespace UI {

    public class LeaderboardScreen : MonoBehaviour {

        [SerializeField]
        private Button _menuButton;

        [SerializeField]
        private UIManager _uimanager;

        [SerializeField]
        private RecordView _recordView;

        [SerializeField]
        private GameObject _table;

        private List<GameObject> _saveRecord;

        private void OnMenuButtonClick() {
            _uimanager.LoadMenu();
        }

        private void Awake() {
            _menuButton.onClick.AddListener(OnMenuButtonClick);
            _saveRecord = new List<GameObject>();
        }

        private void OnEnable() {
            for (var i = 0; Save.SavedDatas.Count > i; i++) {
                var Record = Instantiate(_recordView, _table.transform);
                Record.SetData(i+1, Save.SavedDatas[i].date, Save.SavedDatas[i].score);
                _saveRecord.Add(Record.gameObject);
                if (Save.savePosition == i) {
                    Record.CurrentResult();
                }
            }
        }

        private void OnDisable() {
            for (var i = 0; _saveRecord.Count > i; i++) {
                var temp = _saveRecord[i];
                _saveRecord.RemoveAt(i);
                Destroy(temp);
            }
        }
    }
}