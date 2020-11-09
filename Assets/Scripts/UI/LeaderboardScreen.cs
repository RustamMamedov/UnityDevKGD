using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class LeaderboardScreen : MonoBehaviour {

        [SerializeField]
        private Button _menuButton;

        [SerializeField]
        private GameObject _bestRideRecord;

        [SerializeField]
        private Transform _records;

        private void Awake() {
            _menuButton.onClick.AddListener(OnMenuButtonClick);
        }

        private void OnEnable() {
            for (int i = 0; i < Save.SavedDatas.Count; i++) {
                var record = Save.SavedDatas[i];
                Instantiate(_bestRideRecord, _records).GetComponent<RecordView>().SetData(i + 1, record.date, record.score, record.newRec);
            }
        }

        private void OnDisable() {
            for (int i = 0; i < Save.SavedDatas.Count; i++) {
                Save.SavedDatas[i].newRec = false;
                Destroy(_records.GetChild(i).gameObject);
            }
        }

        private void OnMenuButtonClick() {
            UIManager.Instance.LoadMenu();
        }
    }
}