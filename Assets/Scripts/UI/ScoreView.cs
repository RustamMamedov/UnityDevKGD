using System;
using System.Collections;
using UnityEngine;
using Events;
using Game;
using UnityEngine.UI;

namespace UI {
    public class ScoreView : MonoBehaviour {
        [SerializeField]
        private EventListeners _eventListeners;
        [SerializeField]
        private ScriptableIntValue _currentScore;
        private int _currentscore = 0;
        [SerializeField]
        private Text _lableValue;

        private void Awake() {
            _eventListeners.OnEventHappened += UpdateBehaviour;
        }
        private void UpdateBehaviour() { 
          if(_currentScore.value > _currentscore) {
                StartCoroutine(SetScoreCoroutine(_currentScore.value));
            }
        }
        private IEnumerator SetScoreCoroutine(int score) {
            while (score > _currentscore) {
                _currentscore ++;
                _lableValue.text = $"{_currentscore}"; 
                yield return new WaitForSeconds(0.01f);
            }  
        }
        private void OnEnable() {
            _currentscore = 0;
            _currentScore.value = 0;
            _lableValue.text = $"{_currentscore}";
        }
    }

}