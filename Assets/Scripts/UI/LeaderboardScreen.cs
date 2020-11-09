using UnityEngine;
using UnityEngine.UI;
using Game;

namespace UI {
    public class LeaderboardScreen : MonoBehaviour {

        [SerializeField]
        private Button _menuButton;

        [SerializeField]
        private GameObject _resultViewPrefab;

        [SerializeField]
        private Transform _resultsKeeper;

        private int _recordCount;

        private void OnEnable() {
            _recordCount = Save.SaveDatas.Count;
            for (int i = 0; i < _recordCount; i++) {
                var tmp = Instantiate(_resultViewPrefab, _resultsKeeper);
                tmp.GetComponent<RecordView>().SetData(i + 1, Save.SaveDatas[i].date, Save.SaveDatas[i].score, i + 1 == Save.CurrentRecordPos);
            }
        }

        private void OnDisable() {
            for (int i = 0; i < _recordCount; i++) {
                Destroy(_resultsKeeper.GetChild(i).gameObject);
            }
        }

        private void Awake() {
            _menuButton.onClick.AddListener(OnMenuButtonClick);
        }

        private void OnMenuButtonClick() {
            UIManager.Instance.LoadMenu();
        }
    }
}