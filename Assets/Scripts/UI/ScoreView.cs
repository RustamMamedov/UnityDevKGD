﻿using System.Collections;
using Events;
using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public sealed class ScoreView : MonoBehaviour {

        [SerializeField]
        private float _scoreCountDelay;

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private ScriptableIntValue _currentScoreValue;

        [SerializeField]
        private Text _scoreLabel;

        private int _currentScore = 0;

        // blocks the start of several coroutines so that changes do not overlap each other
        private bool _scoreIsChanging = false;

        private void Awake() {
            _updateEventListener.OnEventHappened += UpdateBehavior;
        }

        private void UpdateBehavior() {
            if (_currentScore != _currentScoreValue.value && !_scoreIsChanging) {
                _scoreIsChanging = true;
                StartCoroutine(SetScoreCoroutine(_currentScoreValue.value));
            }
        }

        public IEnumerator SetScoreCoroutine(int target) {
            while (_currentScore != target) {
                if (target == 0) {
                    _currentScore = 0;
                } else {
                    ++_currentScore;
                }

                _scoreLabel.text = $"{_currentScore}";
                yield return new WaitForSeconds(_scoreCountDelay);
            }

            _scoreIsChanging = false;
        }
    }
}