using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Values;
using Events;

namespace UI {
    public class ScoreView : MonoBehaviour
    {
        private int _currentScore;

        [SerializeField]
        private ScriptableIntValue _valueScore;

        [SerializeField]
        private EventListener _updateEventListener;
        private void Start() {
            _updateEventListener.OnEventHappened += UpdateBehaviour;
        }
        public void UpdateBehaviour() {
            if(_valueScore.score >= _currentScore) {
            StartCoroutine(SetScoreCoroutine(_valueScore.score));    
            }
            Debug.Log(_currentScore);
        
        }

        private IEnumerator SetScoreCoroutine(int valueScore) {
            while(valueScore > _currentScore) {
                _currentScore++;
                yield return new WaitForSeconds(0.1f);
            }
        } 
    }
}