using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using Events;

namespace UI {
    public class ScoreView : MonoBehaviour {
        private int _currentScore;

        [SerializeField]
        ScriptableIntValue CurrentScore;

        [SerializeField]
        EventListener _eventListener;

        public void UpdateBehaviour() {
            if (CurrentScore.value > _currentScore){
                StartCoroutine(SetScoreCoroutine(CurrentScore.value));
            }
        }

        private IEnumerator SetScoreCoroutine(int score) {
            while (_currentScore != score){
                _currentScore += 1;
                Debug.Log(_currentScore);
                yield return new WaitForSeconds(1f);
            }
        }
    }
}