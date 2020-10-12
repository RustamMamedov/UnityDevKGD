using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Events;
using Game;
using UnityEngine.UI;

namespace UI {
    public class ScoreView : MonoBehaviour {

        [SerializeField]
        private float _currentDelay;

        [SerializeField]
        private Text _scoreLabel;


        private int _currentScore;
        private bool isBusy;
        
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
    
            yield return new WaitForSeconds(_currentDelay);
            if (_currentScore < _scores.value) {
                _currentScore += 1;
                _scoreLabel.text = $"{_currentScore}";
                
            }
        }
    }
}