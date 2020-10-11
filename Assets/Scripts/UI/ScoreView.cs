using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Game;

namespace UI {

    public class ScoreView : MonoBehaviour {

        [SerializeField]
        private ScriptableIntValue CurrentScore;

        [SerializeField]
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
                Debug.Log(_currentScore);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}