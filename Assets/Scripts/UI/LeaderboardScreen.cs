using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game;

namespace UI {
    public class LeaderboardScreen : MonoBehaviour {
        [SerializeField]
        private Button _menu;

        //[SerializeField]
        //private UIManager _uiManager;

        [SerializeField]
        private ResultView _resultView;

        [SerializeField]
        private GameObject _playersList;

        private List<GameObject> _listResultView = new List<GameObject>();
        private void Awake() {
            _menu.onClick.AddListener(OnPlayButtonClick);
        }
        private void OnPlayButtonClick() {
            //_uiManager.LoadMenu();
            if (UIManager.Instance != null) {
                UIManager.Instance.LoadMenu();
            }
        }
        private void OnEnable() {
            Filling();
        }

        private void OnDisable() {
            DeleteListResult();
        }

        private void DeleteListResult() {
            for (int i = _listResultView.Count - 1; i >= 0; i--) {
                Destroy(_listResultView[i]);
                _listResultView.RemoveAt(i);
            }
        }

        private void Filling() {
            for (int i = 0; i < Save.savedDatas.Count; i++) {
                var newResult = Instantiate(_resultView, _playersList.transform);
                newResult.SetData(i + 1, Save.savedDatas[i].date, Save.savedDatas[i].score);
                if (i == Save._last) newResult.Symbol();
                _listResultView.Add(newResult.gameObject);
                Debug.Log($"{Save._last} rrr");
                newResult.gameObject.SetActive(true);
            }
        }
    }
}