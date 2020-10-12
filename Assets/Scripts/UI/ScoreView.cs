using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using Events;

namespace UI {

    public class ScoreView : MonoBehaviour {

        [SerializeField]
        private int _currentScore;

        [SerializeField]
        private ScriptableIntValue _assetScore;

        [SerializeField]
        private EventListener _updateEventListener;

        private void Awake() {
            _updateEventListener.OnEventHappened += UpdateBehavior;
        }

        public void UpdateBehavior() {
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