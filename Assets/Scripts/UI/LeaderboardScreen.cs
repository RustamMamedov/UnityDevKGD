using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class LeaderboardScreen : MonoBehaviour {

        [SerializeField]
        private Button _menuButton;

        [SerializeField]
        private GameObject _leaderboardTable;

        [SerializeField]
        private GameObject _resultView;

        private void Awake() {
            _menuButton.onClick.AddListener(OnClickMenuButton);
        }

        private void OnEnable() {
            UpdateRecords();
        }

        private void OnDisable() {
            ClearRecords();
        }

        private void UpdateRecords() {
            var place = 1;

            if (Save.SavedData != null) {
                foreach (Save.SaveData save in Save.SavedData) {
                    var recordView = Instantiate(_resultView, _leaderboardTable.transform);
                    recordView.GetComponent<RecordView>().SetData(place, save.date, save.score);

                    place++;
                }
            }
        }

        private void ClearRecords() {
            for (int i = _leaderboardTable.transform.childCount - 1; i >= 0; i--) {
                var recordView = (_leaderboardTable.transform.GetChild(i));
                recordView.SetParent(null, false);
                Destroy(recordView.gameObject);
            }
        }

        private void OnClickMenuButton() {
            UIManager.instance.LoadMenu();
        }
    }
}
