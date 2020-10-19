using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Game;
using UnityEngine.UI;

namespace UI {
    public class ScoreView: MonoBehaviour {

        [SerializeField] private float _scoreCountDelay;
        private int _currentScore; 
        [SerializeField] ScriptableIntValue _assetScore;
        [SerializeField] EventListener _updateEventListener;
        [SerializeField] Text _scoreLabel;

        private void Awake() {
            _updateEventListener.OnEventHappened += UpdateBehaviour;
        }

        private void UpdateBehaviour() {
            if (_currentScore < _assetScore.value) {
                StartCoroutine(SetScoreCoroutine(_assetScore.value));
            }
        }

        private IEnumerator SetScoreCoroutine(int assetScore) {
            while (_currentScore != assetScore) {
                if (_currentScore < assetScore) {
                    _currentScore++;
                }
                _scoreLabel.text = $"{_currentScore}";
                yield return new WaitForSeconds(_scoreCountDelay);
            }
        }
    } 
}