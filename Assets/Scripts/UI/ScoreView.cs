using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
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
        private bool isBusy;

        private void Awake() {
            _eventListener.OnEventHappened += UpdateBehaviour;
        }

        private void UpdateBehaviour() {
            if (_currentScoreValue.value > _currentScore && !isBusy) {
                StartCoroutine(SetScoreCoroutine(_currentScoreValue.value));
            }
        }

        private IEnumerator SetScoreCoroutine(int score) {
            isBusy = true;
            while (_currentScore != score){
                _currentScore++;
                _scoreLabel.text = $"{_currentScore}";
                Debug.Log(_currentScore);
                yield return new WaitForSeconds(_scoreCountDelay);
            }
            isBusy = false;
        }
    }
}