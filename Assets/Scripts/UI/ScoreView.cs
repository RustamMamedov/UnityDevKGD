using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
    public class ScoreView : MonoBehaviour
    {
        private int  _currentScore;
        
        private ScriptableIntValue _valueScore;
        public void UpdateBehaviour() {
            while (CurrentScore >= _currentScore) {
            StartCorutine(SetScoreCoroutine(CurrentScore));
            }
        }

        private IEnumerator SetScoreCoroutine(CurrentScore) {
            while(_currentScore < CurrentScore) {
                _currentScore++;
            }
        } 
    }
}