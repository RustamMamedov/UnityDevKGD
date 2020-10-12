using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Events;

namespace UI {
    public class ScoreView:MonoBehaviour {
        [SerializeField]
        private int _currentScore;
        [SerializeField]
        private CurrentScore _Scores;
        [SerializeField]
        private EventListener _eventListener;
        private void Awake() {
            _eventListener.OnEventHappened += UpdateBehaviour;
        }
        public void UpdateBehaviour() {
            if(_currentScore > _Scores.save_score) {
                StartCoroutine(SetScoreCoroutine());
            }
        }
        private IEnumerator SetScoreCoroutine() {
            yield return new WaitForSeconds(0.1f);
            if(_currentScore > _Scores.save_score) {
                _currentScore += 1;
                Debug.Log(_currentScore);
            }
        }
    }
}