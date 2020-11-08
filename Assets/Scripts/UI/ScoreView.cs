using System;
using System.Collections;
using Events;
using Game;
using UnityEngine;
using UnityEngine.UI;

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
        
        //Reference to UI element where score will be displayd
        [SerializeField] 
        private Text _scoreLabel;
    
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

        private void OnEnable() {
            _currentScore = 0;
        }

        private void OnDestroy() {
            _eventListener.OnEventHappened -= UpdateBehaviour;
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

                _scoreLabel.text = $"{_currentScore}";
                yield return new WaitForSeconds(_scoreUpdatingDelay);
            }

            _isScoreChanging = false;
        }
        #endregion
        
    }
    
}


