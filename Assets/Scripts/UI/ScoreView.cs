using System.Collections;
using Events;
using Game;
using UnityEngine;

namespace UI {

    public class ScoreView : MonoBehaviour {

        [SerializeField]
        private ScriptableIntValue _scriptableIntValue;

        [SerializeField]
        private EventListener _eventListener;
        [SerializeField]
        private ScriptableEvent _event;

        private uint _currentScore = 0;

        
        private void Awake() {
            _eventListener.onEventHappened += UpdateBehavior;
        }

        private void UpdateBehavior() {

            while (_currentScore < _scriptableIntValue.currentSсore) {
                StartCoroutine(SetScoreCoroutine(_scriptableIntValue.currentSсore));
            }

        }

        private IEnumerator SetScoreCoroutine(int score) { 
             Debug.Log(_currentScore);  
           
            while (_currentScore != score) {      
                _currentScore++; 
                yield return new WaitForSeconds(0.1f); 
            } 
            
        } 

    }
}