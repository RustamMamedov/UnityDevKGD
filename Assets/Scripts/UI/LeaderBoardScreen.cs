using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class LeaderBoardScreen : MonoBehaviour {

        [SerializeField] 
        private Button _startGameButton;
        
        [SerializeField] 
        private RecordView _recordViewTemplate;
        
        [SerializeField] 
        private Transform _containerForResultViews;
        
        [SerializeField]
        private ScriptableIntValue _currentScorePosition;
        
        private void Awake() {
            _startGameButton.onClick.AddListener(OnPlayButtonClick);
        }

        private void OnEnable() {
            
            for (int i = 0; i < Save.SavedDatas.Count; i++) {
                bool isCurrent = _currentScorePosition.value == i;
                var resultView = Instantiate(_recordViewTemplate, _containerForResultViews);
                resultView.SetData(i + 1,Save.SavedDatas[i].date, Save.SavedDatas[i].score, isCurrent);
            }
        }

        private void OnDisable() {
            for (int i = Save.SavedDatas.Count - 1; i > -1; i--) {
                Destroy(_containerForResultViews.GetChild(i).gameObject);
            }
        }

        private void OnPlayButtonClick() {
            UIManager.Instance.LoadMenu();
        }
    }
}