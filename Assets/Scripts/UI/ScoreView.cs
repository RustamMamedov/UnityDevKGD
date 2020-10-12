using UnityEngine;
using System.Collections;
using Game;
using Events;
namespace UI {

    public class ScoreView : MonoBehaviour {

        public ScriptableIntValue currentScore;

        private int _currentScore;

        private bool isScoreChanging = false;

        public EventListener eventListener;

        private void Start() {
            eventListener.OnEventHappened += UpdateBehaviour;
        }

        private void UpdateBehaviour() {
            if (currentScore.value > _currentScore && !isScoreChanging) {
                StartCoroutine(SetScoreCoroutine(currentScore.value));
            }
        }
        
        private IEnumerator SetScoreCoroutine(int newScore) {
            isScoreChanging = true;
            while(_currentScore < newScore) {
                _currentScore++;
                Debug.Log(_currentScore);
                yield return new WaitForSeconds(0.1f);
            }
            isScoreChanging = false;
        }
    }

}
