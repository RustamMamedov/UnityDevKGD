using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Events;
using Game;

namespace UI {
    public class ScoreView : MonoBehaviour {
        
        [SerializeField]
        private int _currentScore;
        
        [SerializeField]
        private ScriptableIntValue _scores;
        
        [SerializeField]
        private EventListener _eventListener;

        private void Awake() {
            _eventListener.OnEventHappened += Update;

        }


        public void Update() {
            if (_currentScore < _scores.value) {
                StartCoroutine(SetScoreCoroutine());
            }
            
            
        }
        private IEnumerator SetScoreCoroutine() {
            yield return new WaitForSeconds(0.1f);
            if (_currentScore < _scores.value) {
                _currentScore += 1;
                Debug.Log(_currentScore);
            }
        }
    }
}