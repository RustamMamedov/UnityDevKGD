using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Game;
using Events;
namespace UI {

    public class ScoreView : MonoBehaviour {
        
        [SerializeField]
        private float _scoreCountDelay;

        [SerializeField]
        private Text _scoreLabel;

        [SerializeField]
        private ScriptableIntValue _currentScore;

        private int _currentScoreView;

        private bool isScoreChanging = false;

        [SerializeField]
        private EventListener _eventListener;

        private void OnEnable() {
            _eventListener.OnEventHappened += UpdateBehaviour;
        }

        private void OnDisable() {
            _currentScoreView = 0;
            _scoreLabel.text = $"{_currentScoreView}";
            _eventListener.OnEventHappened -= UpdateBehaviour;
        }
        private void UpdateBehaviour() {
            if (_currentScore.value > _currentScoreView && !isScoreChanging) {
                StartCoroutine(SetScoreCoroutine(_currentScore.value));
            }
        }
        
        private IEnumerator SetScoreCoroutine(int newScore) {
            isScoreChanging = true;
            while(_currentScoreView < newScore) {
                _currentScoreView++;
                _scoreLabel.text = $"{_currentScoreView}";
                yield return new WaitForSeconds(_scoreCountDelay);
            }
            isScoreChanging = false;
        }
    }

}
