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
        private ScriptableIntValue _currentScoreValue;

        [SerializeField]
        private EventListener _eventListener;

        private int _currentScore;

        private bool isScoreChanging = false;

        private void OnEnable() {
            _currentScore = _currentScoreValue.value;
            _scoreLabel.text = $"{_currentScore}";
            _eventListener.OnEventHappened += UpdateBehaviour;
        }

        private void UpdateBehaviour() {
            if (_currentScoreValue.value > _currentScore && !isScoreChanging) {
                StartCoroutine(SetScoreCoroutine(_currentScoreValue.value));
            }
        }

        private IEnumerator SetScoreCoroutine(int newScore) {
            isScoreChanging = true;
            while (_currentScore < newScore) {
                _currentScore++;
                _scoreLabel.text = $"{_currentScore}";
                yield return new WaitForSeconds(_scoreCountDelay);
            }
            isScoreChanging = false;
        }
    }
}