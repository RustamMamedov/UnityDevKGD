using Events;
using Game;
using System.Collections;
using UnityEngine;

namespace UI {

    public sealed class ScoreView : MonoBehaviour {

        [SerializeField]
        private EventListener _eventListener;

        [SerializeField]
        private ScriptableIntValue _score;

        private int _currentScore = 0;

        // blocks the start of several coroutines so that changes do not overlap each other
        private bool _scoreIsChanging = false;

        private void Awake() {
            _eventListener.OnEventHappened += UpdateBehavior;
        }

        private void UpdateBehavior() {
            if (_currentScore != _score.value && !_scoreIsChanging) {
                _scoreIsChanging = true;
                StartCoroutine(SetScoreCoroutine(_score.value));
            }
        }

        public IEnumerator SetScoreCoroutine(int target) {
            while (_currentScore != target) {
                if (_currentScore < target) {
                    ++_currentScore;
                } else {
                    --_currentScore;
                }
                Debug.Log(_currentScore);
                yield return new WaitForSeconds(0.1f);
            }

            _scoreIsChanging = false;
        }
    }
}