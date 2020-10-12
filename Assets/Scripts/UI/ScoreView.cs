using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Events;
using Game;

namespace UI {
    public class ScoreView : MonoBehaviour {
        [SerializeField]
        int _currentScore;
        [SerializeField]
        ScriptableIntValue _scores;
        [SerializeField]
        EventListener _eventListener;

        private void Awake() {
            _eventListener.OnEventHappened += UpdateBehaviour;
        }


        public void UpdateBehaviour() {
            if (_currentScore > _scores.value) {
                StartCoroutine(SetScoreCoroutine());
            }
            
            
        }
        private IEnumerator SetScoreCoroutine() {
            yield return new WaitForSeconds(0.1f);
            if (_currentScore > _scores.value) {
                _currentScore += 1;
                Debug.Log(_currentScore);
            }
        }
    }
}