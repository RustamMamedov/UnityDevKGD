using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Game;
using Events;

namespace UI {
    public class ScoreView : MonoBehaviour {
        [SerializeField]
        private float _scoreCountDelay;

        [SerializeField]
        private Text _scoreLabel;

        [SerializeField]
        ScriptableIntValue _currentScoreValue;

        [SerializeField]
        EventListener _eventListener;

        private int _currentScore;

        public void UpdateBehaviour() {
            if (_currentScoreValue.value > _currentScore){
                StartCoroutine(SetScoreCoroutine(_currentScoreValue.value));
            }
        }

        private IEnumerator SetScoreCoroutine(int score) {
            while (_currentScore != score){
                _currentScore++;
                _scoreLabel.text = $"{_currentScore}";
                Debug.Log(_currentScore);
                yield return new WaitForSeconds(_scoreCountDelay);
            }
        }
    }
}