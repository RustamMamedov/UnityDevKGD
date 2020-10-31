using System.Collections;
using Events;
using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class ScoreView : MonoBehaviour {

        [SerializeField]
        private float _scoreCountDelay;

        [SerializeField]
        private ScriptableIntValue _score;

        [SerializeField]
        private EventListener _eventListener;

        [SerializeField]
        private Text _scoreLabel;

        private int _currentScore = 0;

        private bool _scoreIsChanging = false;

        private void OnEnable() {
            _currentScore = _score.value;
            _scoreLabel.text = $"{_currentScore}";
        }

        private void Update() {
            _eventListener.OnEventHappened += UpdateBehaviour;
        }

        public void UpdateBehaviour() {
            if (_score.value > _currentScore && !_scoreIsChanging) {
                StartCoroutine(SetScoreCoroutine(_score.value));
            }
        }

        private IEnumerator SetScoreCoroutine(int newScore) {
            _scoreIsChanging = true;
            while (_currentScore != newScore) {
                _currentScore += 1;
                _scoreLabel.text = $"{_currentScore}";
                yield return new WaitForSeconds(_scoreCountDelay);
            }
            _scoreIsChanging = false;
        }
    }

}
