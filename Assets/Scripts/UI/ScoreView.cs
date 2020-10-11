using System.Collections;
using UnityEngine;

namespace UI {
    public class ScoreView : MonoBehaviour {

        [SerializeField]
        private int _currentScore = 0; 

        public Game.ScriptableIntValue currentScoreAsset;
        public Events.EventListener eventListener;

        private void Awake() {

            eventListener.OnEventHappened += UpdateBehaviour;
        }

        private void UpdateBehaviour() {
        
            if (_currentScore < currentScoreAsset.score) {
                StartCoroutine(SetScoreCoroutine(currentScoreAsset.score));
            }
                
        }

        private IEnumerator SetScoreCoroutine(int gameScore) {

            while (_currentScore < gameScore) {
                _currentScore++;
                Debug.Log(_currentScore);
                yield return new WaitForSeconds(0.1f);
            }
        }

    }
}

