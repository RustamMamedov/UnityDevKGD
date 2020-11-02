using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using Events;
using UnityEngine.UI;

namespace UI {
    public class ScoreView : MonoBehaviour
    {
        private int _currentScore;

        [SerializeField]
        private float _scoreCountDelay;

        [SerializeField]
        private ScriptableIntValue _valueScore;

        [SerializeField]
        private Text _scoreLabel;

        [SerializeField]
        private EventListener _updateEventListener;
        private void Start() {
            _updateEventListener.OnEventHappened += UpdateBehaviour;
        }
        public void UpdateBehaviour() {
            if(_valueScore.value >= _currentScore) {
            StartCoroutine(SetScoreCoroutine(_valueScore.value));    
            }
        
        }

        private IEnumerator SetScoreCoroutine(int valueScore) {
            while(valueScore > _currentScore) {
                _currentScore++;
                _scoreLabel.text = $"{_currentScore}";
                yield return new WaitForSeconds(_scoreCountDelay);
            }
        } 
    }
}