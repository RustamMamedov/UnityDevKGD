using System.Collections;
using Events;
using Game;
using UnityEngine;

namespace UI {

    public class ScoreView : MonoBehaviour {

        [SerializeField]
        private float _scoreCountDelay;

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private ScriptableIntValue _currentScoreValue;

        private int _currentScore;
        private bool isBusy;

        private void Awake() {
            _updateEventListener.OnEventHappened += UpdateBehaviour;
        }

        private void UpdateBehaviour() {
            if (_currentScoreValue.value > _currentScore && !isBusy) {
                StartCoroutine(SetScoreCoroutine(_currentScoreValue.value));
            }
        }

        public IEnumerator SetScoreCoroutine(int score) {
            isBusy = true;
            while (_currentScore < score) {
                _currentScore++;
                Debug.Log(_currentScore);
                yield return new WaitForSeconds(_scoreCountDelay);
            }
            isBusy = false;
        }
    }
}