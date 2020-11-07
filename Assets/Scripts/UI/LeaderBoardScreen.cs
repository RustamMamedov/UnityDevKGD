using Game;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class LeaderBoardScreen : MonoBehaviour {

        [SerializeField]
        private GameObject _resultViewPrefab;

        [SerializeField]
        private Transform _resultsKeeper;

        [SerializeField]
        private Button _menuButton;

        private int _prefabsAmount;


        private void Awake() {
            _menuButton.onClick.AddListener(LoadMenuButton);
        }

        private void OnEnable() {
            _prefabsAmount = Save.SavedDatas.Count;
            for (int i = 0; i < _prefabsAmount; i++) {
                var tmp = Instantiate(_resultViewPrefab, _resultsKeeper);
                tmp.GetComponent<RecordView>().SetData(i + 1, Save.SavedDatas[i].date, Save.SavedDatas[i].score, i + 1 == Save.CurrentRecordPos);

            }
        }

        private void OnDisable() {
            for (int i = 0; i < _prefabsAmount; i++) {
                Destroy(_resultsKeeper.GetChild(i).gameObject);
            }
        }

        public void LoadMenuButton() {
            UIManager.instance.LoadMenu();
        }
    }
}