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

        private int _prefabsAmount;

        private void OnEnable() {
            _prefabsAmount = Save.SavedDatas.Count;
            Debug.Log(_prefabsAmount);
            for (int i = 0; i < _prefabsAmount; i++) {
                Instantiate(_resultViewPrefab, _resultsKeeper);
                GameObject tmp = Instantiate(_resultViewPrefab, _resultsKeeper);
                tmp.GetComponent<RecordView>().SetData(i + 1, Save.SavedDatas[i].date, Save.SavedDatas[i].score);

            }
        }

        private void OnDisable() {
            _prefabsAmount = Save.SavedDatas.Count;
            for (int i = 0; i < _prefabsAmount - 1; i++) {
                Destroy(_resultsKeeper.GetChild(i).gameObject);
                Debug.Log("Deleted!");
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
