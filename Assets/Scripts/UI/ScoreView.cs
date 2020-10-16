﻿using System;
using System.Collections;
using UnityEngine;
using Game;
using Events;

namespace UI {
    public class ScoreView : MonoBehaviour {

        [SerializeField]
        private EventListeners _EventListeners;

        [SerializeField]
        private ScriptableIntValue _CurrentScore;

        private int _currentScore=0;

        public void Awake() {
           _EventListeners.OnEventHappened+=UpdateBehaviour; 
        }

        public void UpdateBehaviour() {
            if (_CurrentScore.Value > _currentScore) {
                StartCoroutine(SetScoreCoroutine(_CurrentScore.Value));
            }
        }

        public IEnumerator SetScoreCoroutine(int score) {
            while (score>_currentScore) {
                _currentScore++;
                Debug.Log(_currentScore);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}