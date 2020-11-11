using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game;
using Events;

namespace UI {

    public class ScoreView : MonoBehaviour {

        [SerializeField]
        private float _ScoreCountDelay;

        [SerializeField]
        private ScriptableIntValue _assetScore;

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private Text _scoreLabel;

        private int _currentScore;
        private bool isBusy;

        private void Awake() {
            OnEnable();  
        }

        private void OnEnable() {
            _updateEventListener.OnEventHappened += UpdateBehavior;
        }

        private void OnDisable() {
            _currentScore = 0;
            _scoreLabel.text = $"{_currentScore}";
        }

        public void UpdateBehavior() {
            if (_currentScore < _assetScore.value && !isBusy) {
                StartCoroutine(SetScoreCoroutine(_assetScore.value));
                return;
            }
        }

        private IEnumerator SetScoreCoroutine(int assetScore) {
            isBusy = true;
            while (_currentScore < assetScore) {
                _currentScore++;
                _scoreLabel.text = $"{_currentScore}";
                yield return new WaitForSeconds(_ScoreCountDelay);
            }
            isBusy = false;
        }

    }
}