using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Game;
using UnityEngine.UI;

namespace UI {

    public class ScoreView : MonoBehaviour {

        [SerializeField]
        private Text _scoreLabel;

        [SerializeField]
        private float _scoreCountDelay;

        [SerializeField]
        private ScriptableIntValue CurrentScore;

        private int _currentScore;

        [SerializeField]
        private EventListener _event;

        private void Awake() {
            _event.OnEventHappened += UpdateBehaviour;
        }

        public void UpdateBehaviour() {
            if (CurrentScore.value > _currentScore) {
                StartCoroutine(SetScoreCoroutine(CurrentScore.value));
            }
        }
        public IEnumerator SetScoreCoroutine(int _score) {
            while (_currentScore != _score) {
                _currentScore++;
                _scoreLabel.text = _currentScore.ToString();
                 yield return new WaitForSeconds(_scoreCountDelay);
            }
        }
    }
}