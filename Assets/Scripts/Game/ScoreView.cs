using Events;
using Game;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace UI {
    public class ScoreView : MonoBehaviour {
        [SerializeField]
        private ScriptableIntValue currentScore;//Не уверен что приватное, протребуется ли нам менять его во время игры?!
        private int _currentScore;
        private bool isBusy;
        [SerializeField]
        private EventListener _update;
        [SerializeField]
        [Range(0f, 3f)]
        private float _scoreCountDelay;
        [SerializeField]
        private Text _scoreLabel;

        private void Awake() {
            _update.OnEventHappened += UpdateBehaviour;
        }

        private void UpdateBehaviour() {
            if (currentScore.value != _currentScore)
                StartCoroutine(SetScoreCoroutine(currentScore.value));
        }

        private IEnumerator SetScoreCoroutine(int score) {
            isBusy = true;
            while (_currentScore != score) {
                if(_currentScore< score) 
                    _currentScore++;
                else
                    _currentScore--;
                _scoreLabel.text = $"{_currentScore}";
                yield return new WaitForSeconds(_scoreCountDelay);
            }
            isBusy = false;
        }
    }
}
