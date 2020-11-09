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
        private GameObject _leaderboardTable;

        [SerializeField]
        private GameObject _resultView;

        private void Awake() {
            _menuButton.onClick.AddListener(OnMenuButtonClick);
        }

        private void OnEnable() {
            UpdateRecords();
        }

        private void OnDisable() {
            ClearRecords();
        }

        private void UpdateRecords() {
            var place = 1;

            if (Save.SavedDatas != null) {
                foreach (Save.SaveData save in Save.SavedDatas) {
                    var recordView = Instantiate(_resultView, _leaderboardTable.transform);
                    var recordViewComponent = recordView.GetComponent<RecordView>();
                    recordViewComponent.SetData(place, save.date, save.score);

                    if (save.newScore) {
                        recordViewComponent.MarkRecord();
                    } else {
                        recordViewComponent.UnmarkRecord();
                    }
                    place++;
                }
            }
        }

        private void ClearRecords() {
            for (int i = _leaderboardTable.transform.childCount - 1; i >= 0; i--) {
                var recordView = _leaderboardTable.transform.GetChild(i);
                recordView.SetParent(null, false);
                Destroy(recordView.gameObject);
            }
        }

        private void OnMenuButtonClick() {
            UIManager.Instance.LoadMenu();
        }
    }
}
