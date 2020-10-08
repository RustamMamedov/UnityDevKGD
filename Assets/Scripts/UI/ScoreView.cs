using Events;
using Game;
using System.Collections;
using UnityEngine;

namespace UI {
    public class ScoreView : MonoBehaviour {

        public ScriptableIntValue currentScoreAsset;
        public EventListener eventListener;

        [SerializeField]
        private int _currentScore = 0;

        private void Start() {
            eventListener.OnEventHappened += UpdateBehaviour;
        }

        private void UpdateBehaviour() {
            if(currentScoreAsset.score > _currentScore) {
                StartCoroutine(SetScoreCoroutine(currentScoreAsset.score));
            }
        }

        private IEnumerator SetScoreCoroutine(int score) {
            while (_currentScore < score) {
                _currentScore++;
                Debug.Log(_currentScore);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
