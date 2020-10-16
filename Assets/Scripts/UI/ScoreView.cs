using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Game;

namespace UI {
    public class ScoreView: MonoBehaviour {

        [SerializeField] private int _currentScore;
        [SerializeField] ScriptableIntValue _assetScore;
        [SerializeField] EventListener _updateEventListener;

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
                Debug.Log(_currentScore);
                yield return new WaitForSeconds(0.1f);
            }
        }
    } 
}