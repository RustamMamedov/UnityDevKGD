using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class ScoreView : MonoBehaviour {

        [SerializeField]
        private float _scoreCountDelay;

        private int _currentScore = 0; 

        [SerializeField]
        private Text _scoreLabel;

        [SerializeField]
        private Game.ScriptableIntValue currentScoreAsset;

        [SerializeField]
        private Events.EventListener eventListener;

        private void Awake() {
            currentScoreAsset.value = 0;
            eventListener.OnEventHappened += UpdateBehaviour;
        }

        private void UpdateBehaviour() {
        
            if (_currentScore < currentScoreAsset.value) {
                StartCoroutine(SetScoreCoroutine(currentScoreAsset.value));
            }
                
        }

        private IEnumerator SetScoreCoroutine(int gameScore) {

            while (_currentScore < gameScore) {
                _currentScore++;
                _scoreLabel.text =  $"{_currentScore}"; //format from int to string
                yield return new WaitForSeconds(_scoreCountDelay);
            }
        }

    }
}

