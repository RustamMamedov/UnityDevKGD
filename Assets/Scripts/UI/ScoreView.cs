using System;
using System.Collections;
using UnityEngine;
using Game;
using Events;

namespace UI {
    public class ScoreView : MonoBehaviour {

        public event Action OnBehaviour = delegate {};

        [SerializeField]
        private ScriptableEvent _someEvent;

        [SerializeField]
        private ScriptableIntValue _CurrentScore;

        private int _currentScore=0;

        public void Awake() {
           _someEvent.AddListener(UpdateBehaviour); 
        }

        public void UpdateBehaviour() {
            if (_CurrentScore.Score > _currentScore) {
                StartCoroutine(SetScoreCoroutine(_CurrentScore.Score));
            }
        }

        public IEnumerator SetScoreCoroutine(int score) {
            while (score>_currentScore) {
                _currentScore++;
                Debug.Log(_currentScore);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}