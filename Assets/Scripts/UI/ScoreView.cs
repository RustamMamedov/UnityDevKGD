using System;
using System.Collections;
using Events;
using Game;
using UnityEngine;

namespace UI {
    
    public class ScoreView : MonoBehaviour {

        #region Fields
        
        // Asset where we store game's score
        [SerializeField] 
        private ScriptableIntValue _currentScoreAsset;
        
        // Display score
        [SerializeField] 
        private int _currentScore;
        
        // Event UpdateBehaviour will be subscribed on
        [SerializeField] 
        private EventListener _eventListener;
    
        [SerializeField] 
        private float _scoreUpdatingDelay = 0.1f;
        
        // Boolean that describes if the changing score coroutine is launched already
        private bool _isScoreChanging = false;
        
        #endregion
        
        #region LifeCycle
        
        private void Awake() {
            // Subscribing on Update event
            _eventListener.OnEventHappened += UpdateBehaviour;
        }
        #endregion

        #region Methods
        
        private void UpdateBehaviour() {
            var targetValue = _currentScoreAsset.value;
            if (targetValue != _currentScore && !_isScoreChanging) {
                _isScoreChanging = true;
                StartCoroutine(SetScoreCoroutine(targetValue));
            }
        }
        
        #endregion
        
        #region Coroutines
        
        // Changing score by 1 per _scoreUpdatingDelay seconds
        private IEnumerator SetScoreCoroutine(int target) {
            while (_currentScore != target) {
                if (_currentScore < target) {
                    ++_currentScore;
                } else {
                    --_currentScore;
                }
                
                Debug.Log("Score: " + _currentScore);
                yield return new WaitForSeconds(_scoreUpdatingDelay);
            }

            _isScoreChanging = false;
        }
        #endregion
        
    }
    
}


