using System;
using System.Collections;
using UnityEngine;
using Game;
using Events;
using UnityEngine.UI;

namespace UI {

    public class ScoreView : MonoBehaviour {

        [SerializeField]
        private float _scoreCountDelay;

        [SerializeField]
        private EventListeners _eventListeners;

        [SerializeField]
        private ScriptableIntValue _сurrentScoreValue;

        [SerializeField]
        private Text _scoreLabel;

        private int _currentScore=0;
        private bool _isBusy=false;

        public void Awake() {
           _eventListeners.OnEventHappened+=UpdateBehaviour; 
        }

        public void OnDestroy() {
            _eventListeners.OnEventHappened -= UpdateBehaviour;
        }

        private void OnEnable() {
            _сurrentScoreValue.Value = 0;
            _currentScore = 0;
            _scoreLabel.text = $"{ _currentScore}";
        }

        public void UpdateBehaviour() {
            if (_сurrentScoreValue.Value > _currentScore&&!_isBusy) {
                StartCoroutine(SetScoreCoroutine(_сurrentScoreValue.Value));
            }
        }

        public IEnumerator SetScoreCoroutine(int score) {
            _isBusy = true;
            while (score>_currentScore) {
                _currentScore++;
                _scoreLabel.text = $"{ _currentScore}";
                yield return new WaitForSeconds(_scoreCountDelay);
            }
            _isBusy = false;
        }
    }
}