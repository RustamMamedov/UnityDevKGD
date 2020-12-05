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
        [SerializeField]
        private ScriptableIntValue _currentScore; 
        [SerializeField] ScriptableIntValue _assetScore;
        [SerializeField] EventListener _updateEventListener;
        [SerializeField] Text _scoreLabel;

        private void Awake() {
            _updateEventListener.OnEventHappened += UpdateBehaviour;
        }

        private void UpdateBehaviour() {
            if (_currentScore.value < _assetScore.value) {
                StartCoroutine(SetScoreCoroutine(_assetScore.value));
            }
        }

        private IEnumerator SetScoreCoroutine(int assetScore) {
            while (_currentScore.value != assetScore) {
                if (_currentScore.value < assetScore) {
                    _currentScore.value++;
                }
                _scoreLabel.text = $"{_currentScore.value}";
                yield return new WaitForSeconds(_scoreCountDelay);
            }
        }
    } 
}