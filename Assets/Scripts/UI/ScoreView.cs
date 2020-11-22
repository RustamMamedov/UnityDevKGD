using Events;
using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class ScoreView : MonoBehaviour {

        [SerializeField]
        private float _scoreCountDelay;
        [SerializeField]
        private ScriptableIntValue _currentScoreAsset;

        [SerializeField]
        private EventListener _eventListener;

        [SerializeField]
        private Text _scoreLabel;

        private int _currentScore = 0;
        private bool _isScoreChanging = false;

        private void OnEnable() {
            _currentScore = _currentScoreAsset.value;
            _scoreLabel.text = $"{_currentScore}";
        }

        private void Awake() {
            _eventListener.OnEventHappened += UpdateBehaviour;
        }

        public void UpdateBehaviour() {
            if (_currentScoreAsset.value > _currentScore && !_isScoreChanging) {
                _isScoreChanging = true;
                StartCoroutine(SetScoreCoroutine(_currentScoreAsset.value));
            }
        }

        private IEnumerator SetScoreCoroutine(int targetScore) {
            while (_currentScore != targetScore) {
                _currentScore++;
                _scoreLabel.text = $"{_currentScore}";

                yield return new WaitForSeconds(_scoreCountDelay);
            }
            _isScoreChanging = false;
        }
    }
}

