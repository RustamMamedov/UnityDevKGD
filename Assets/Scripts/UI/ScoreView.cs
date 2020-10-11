using System.Collections;
using UnityEngine;
using Game;
using Events;

namespace UI {
    public class ScoreView : MonoBehaviour {

        [SerializeField]
        private ScriptableIntValue _currentScoreAsset;

        [SerializeField]
        private int _currentScore;

        [SerializeField]
        private EventListener _eventListener;

        [SerializeField]
        private float _currentScoreDelay = 0.1f;

        private int _scoreChanged;

        private bool _isStartCoroutine = false;
        private bool _isCurrentScoreChanged = true;

        private void Awake() {
            _eventListener.OnEventHappened += UpdateBehaviour;
            _scoreChanged = _currentScoreAsset._score;
        }

        private void UpdateBehaviour() {
            var target =_currentScore;

            if (_scoreChanged != _currentScoreAsset._score) {
                _isCurrentScoreChanged = true;
                _scoreChanged = _currentScoreAsset._score;
            }

            if (target < _currentScoreAsset._score && _isCurrentScoreChanged) {
                _isCurrentScoreChanged = false;
                _isStartCoroutine = true;
                target = _currentScoreAsset._score;
            }

            if (_isStartCoroutine) {
                _isStartCoroutine = false;
                Debug.Log(_currentScore + " -> " + target);
                StartCoroutine(SetScoreCoroutine(target));

            }

        }

        private IEnumerator SetScoreCoroutine(int target) {
            
            while (_currentScore < target) {
                _currentScore++;
                Debug.Log(_currentScore + " -> " + target);
                if (target!= _currentScoreAsset._score) {
                    target = _currentScoreAsset._score;
                }
                yield return new WaitForSeconds(_currentScoreDelay);
            }

        }
    }

}


