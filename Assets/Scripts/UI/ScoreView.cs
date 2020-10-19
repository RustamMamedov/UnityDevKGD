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
        private ScriptTableIntValue _CurrentScore;
        private int _currentscore;
        [SerializeField]
        private Text _lableValue;

        public void Awake() {
            _eventListeners.OnEventHappened += UpdateBehaviour;
        }
        public void UpdateBehaviour() { 
          if(_CurrentScore.Score > _currentscore) {
                StartCoroutine(SetScoreCoroutine(_CurrentScore.Score));
            }
        }
        public IEnumerator SetScoreCoroutine(int score) {
            while (score > _currentscore) {
                _currentscore ++;
                _lableValue.text = $"{_currentscore}"; 
                yield return new WaitForSeconds(0.1f);
            }  
        }
    }
}