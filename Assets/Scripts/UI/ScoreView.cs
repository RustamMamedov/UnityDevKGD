using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            _updateEventListener.OnEventHappened += UpdateBehavior;
        }

        public void UpdateBehavior() {
            if (_currentScore < _assetScore.value && !isBusy) {
               StartCoroutine(SetScoreCoroutine(_assetScore.value));
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