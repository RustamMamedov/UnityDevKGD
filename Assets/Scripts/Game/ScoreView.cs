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

        private bool _isBusy;

        private void Awake() {
            _update.OnEventHappened += UpdateBehaviour;
        }

        private void OnEnable() {
            _currentScoreScriptable.value = 0;
            _currentScore = 0;
            _scoreLabel.text = $"{_currentScore}";
            _isBusy = false;
        }

        private void UpdateBehaviour() {
            if ((_currentScoreScriptable.value > _currentScore)&&(!_isBusy))
                StartCoroutine(SetScoreCoroutine(_currentScoreScriptable.value));
        }

        private IEnumerator SetScoreCoroutine(int score) {
            _isBusy = true;
            while (_currentScore < score) {
                if(_currentScore< score) 
                    _currentScore++;
                _scoreLabel.text = $"{_currentScore}";
                yield return new WaitForSeconds(_scoreCountDelay);
            }
            _isBusy = false;
        }
    }
}
