using UnityEngine;
using UnityEngine.UI;
using Game;
using System.Collections.Generic;

namespace UI {
    
    public class LeaderboardScreen : MonoBehaviour {

        [SerializeField]
        private Button _menuButton;

        [SerializeField]
        private GameObject _resultViewPrefab;

        [SerializeField]
        private GameObject _playersField;

        private List<GameObject> _scoreBoard = new List<GameObject>{};

        private void OnEnable() {
            _menuButton.onClick.AddListener(OnMenuButtonClick);
            var saveDatas = Save.SavedDatas;
            int place = 1;
            for (int i = saveDatas.Count - 1; i >= 0; i--) {
                var resultView = Instantiate(_resultViewPrefab, _playersField.transform);
                resultView.GetComponent<ResultView>().SetData(place, saveDatas[i].date, saveDatas[i].score);

                if (Save.CurrentPlace == i) {
                    resultView.GetComponent<ResultView>().SetNewScoreImage();
                }
                
                _scoreBoard.Add(resultView);
                place++;
            }
        }

        private void OnDisable() {
            for (int i = _scoreBoard.Count - 1; i >= 0; i--) {
                Destroy(_scoreBoard[i]);
                _scoreBoard.RemoveAt(i);
            }
        }

        private void OnMenuButtonClick() {
            UIManager.Instance.LoadMenu();
            _menuButton.onClick.RemoveAllListeners();
        }
    }
}