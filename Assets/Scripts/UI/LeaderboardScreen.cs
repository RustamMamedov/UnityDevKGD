using Game;
using Audio;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class LeaderboardScreen : MonoBehaviour {

        [SerializeField]
        private Button _menuButton;

        [SerializeField]
        private Transform _resultViewParent;

        [SerializeField]
        private RecordView _resultViewPrefab;

        [SerializeField]
        private ScriptableIntValue _indexLast;

        private RecordView[] _recordViews;

        private void Awake() {
            _menuButton.onClick.AddListener(OnMenuButtonClick);
        }

        private void OnEnable() {
            CreateTable();
        }

        private void OnDisable() {
            DeleteTable();
        }

        private void CreateTable() {
            _recordViews = new RecordView[Save.SavedDatas.Count];
            for (int i = 0; i < _recordViews.Length; i++) {

                var recordView = Instantiate(_resultViewPrefab, _resultViewParent);
                recordView.SetData(i + 1, Save.SavedDatas[i].date, Save.SavedDatas[i].score, _indexLast.value == i);

                _recordViews[i] = recordView;
            }
        }

        private void DeleteTable() {
            for (int i = _recordViews.Length - 1; i >= 0; i--) {
                Destroy(_recordViews[i].gameObject);
            }
            _recordViews = null;
        }

        private void OnMenuButtonClick() {
            UIManager.Instance.LoadMenu();
        }
    }
}