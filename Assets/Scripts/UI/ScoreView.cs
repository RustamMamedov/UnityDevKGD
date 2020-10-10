using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Values;

namespace UI {
    public class ScoreView : MonoBehaviour
    {
        private int _currentScore;
        
        private ScriptableIntValue _valueScore;

        public void UpdateBehaviour() {
            while (_valueScore.score >= _currentScore) {
            StartCoroutine(SetScoreCoroutine(_valueScore.score));
            Debug.Log(_currentScore);
            }
        
        }

        private IEnumerator SetScoreCoroutine(int valueScore) {
            while(_currentScore < valueScore) {
                _currentScore++;
                yield return new WaitForSeconds(0.1f);
            }
        } 
    }
}