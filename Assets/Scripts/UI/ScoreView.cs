using UnityEngine;
using System.Collections;
using Game;
using Events;
namespace UI {

    public class ScoreView : MonoBehaviour {

        public ScriptableIntValue currentScore;

        private int _currentScore;

        public EventListener eventListener;

        private void Start() {
            eventListener.OnEventHappened += UpdateBehaviour;
        }

        private void UpdateBehaviour() {
            if(currentScore.score > _currentScore) {
                StartCoroutine(SetScoreCoroutine(currentScore.score));
            }
        }
        
        private IEnumerator SetScoreCoroutine(int newScore) {

            while(_currentScore < newScore) {
                _currentScore++;
                Debug.Log(_currentScore);
                yield return new WaitForSeconds(0.1f);
            }

        }
    }

}
