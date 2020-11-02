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
        private ScriptableIntValue _currentScoreScriptable;//Не уверен что приватное, протребуется ли нам менять его во время игры?!
        private int _currentScore;
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

        private void OnEnable() {
            _currentScoreScriptable.value = 0;
        }

        private void UpdateBehaviour() {
            if (_currentScoreScriptable.value != _currentScore)
                StartCoroutine(SetScoreCoroutine(_currentScoreScriptable.value));
        }

        private IEnumerator SetScoreCoroutine(int score) {
            while (_currentScore != score) {
                if(_currentScore< score) 
                    _currentScore++;
                else
                    _currentScore--;
                _scoreLabel.text = $"{_currentScore}";
                yield return new WaitForSeconds(_scoreCountDelay);
            }
        }
    }
}
