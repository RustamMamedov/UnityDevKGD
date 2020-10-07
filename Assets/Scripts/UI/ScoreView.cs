﻿using System.Collections;
using Events;
using Game;
using UnityEngine;

namespace UI {

    public class ScoreView : MonoBehaviour {

        [SerializeField]
        private ScriptableIntValue _score;

        [SerializeField]
        private EventListener _eventListener;

        private int _currentScore = 0;

        private bool scoreIsChanging = false;

        private void Update() {
            _eventListener.OnEventHappened += UpdateBehaviour;
        }

        public void UpdateBehaviour() {
            if (_score.score > _currentScore && !scoreIsChanging) {
                StartCoroutine(SetScoreCoroutine(_score.score));
            }
        }

        private IEnumerator SetScoreCoroutine(int newScore) {
            scoreIsChanging = true;
            while (_currentScore != newScore) {
                _currentScore += 1;
                Debug.Log(_currentScore);
                yield return new WaitForSeconds(0.1f);
            }
            scoreIsChanging = false;
        }
    }

}
