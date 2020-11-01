using System.Collections;
using UnityEngine;
using Game;
using Events;
using UnityEngine.UI;

namespace UI {
    public class ScoreView : MonoBehaviour {

        [SerializeField]
        private ScriptableIntValue _currentScoreAsset;


        [SerializeField]
        private EventListener _eventListener;

        [SerializeField]
        private float _currentScoreDelay = 0.1f;

        [SerializeField]
        private Text _typeCarLabel1;

        [SerializeField]
        private Text _typeCarLabel2;

        [SerializeField]
        private Text _typeCarLabel3;

        [SerializeField]
        private Text _scoreLabel;

        private int _scoreChanged;
        private int _currentScore;

        private bool _isStartCoroutine = false;
        private bool _isCurrentScoreChanged = true;

        private void Awake() {
            _eventListener.OnEventHappened += UpdateBehaviour;
            _scoreChanged = _currentScoreAsset.value;
        }

        private void UpdateBehaviour() {
            var target =_currentScore;

            if (_scoreChanged != _currentScoreAsset.value) {
                _isCurrentScoreChanged = true;
                _scoreChanged = _currentScoreAsset.value;
            }

            if (target < _currentScoreAsset.value && _isCurrentScoreChanged) {
                _isCurrentScoreChanged = false;
                _isStartCoroutine = true;
                target = _currentScoreAsset.value;
            }

            if (_isStartCoroutine) {
                _isStartCoroutine = false;

                StartCoroutine(SetScoreCoroutine(target));

            }

        }

        private IEnumerator SetScoreCoroutine(int target) {
            
            while (_currentScore < target) {
                _currentScore++;
                _scoreLabel.text = $"{_currentScore}";
                if (target!= _currentScoreAsset.value) {
                    target = _currentScoreAsset.value;
                }
                yield return new WaitForSeconds(_currentScoreDelay);
            }

        }
    }

}


