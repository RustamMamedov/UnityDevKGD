using System.Collections;
using System.Collections.Generic;
using Events;
using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class ScoreView : MonoBehaviour {

        [SerializeField]
        private float _scoreCountDelay;

        [SerializeField]
        private EventListener _update;

        [SerializeField]
        private ScriptableIntValue _currentScoreValue;

        [SerializeField]
        private Text _scoreLabel;

        private int _currentScore = 0;

        private bool isBusy;

        private void Awake() {
            _update.OnEventHappened += UpdateBehaviour;
        }

        private void UpdateBehaviour() {
            if (_currentScoreValue.value != _currentScore && !isBusy) {
                StartCoroutine(SetScoreCoroutine(_currentScoreValue.value));
            }
        }

        private IEnumerator SetScoreCoroutine(int score) {
            isBusy = true;

            if (_currentScore > score) {
                _scoreLabel.text = $"{score}";
                _currentScore = score;
            }

            while (_currentScore < score) {
                _currentScore += PlayerCar.DodgeScore;
                _scoreLabel.text = $"{_currentScore}";
                yield return new WaitForSeconds(_scoreCountDelay);
            }

            isBusy = false;
        }
    }
}