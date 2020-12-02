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
        private Game.ScriptableIntValue _currentScoreAsset;

        [SerializeField]
        private Events.EventListener _eventListener;

        private void Awake() {
            _currentScoreAsset.value = 0;
            _scoreLabel.text = _currentScoreAsset.value.ToString();
            _eventListener.OnEventHappened += UpdateBehaviour;
        }

        private void OnDisable() {
            _currentScoreAsset.value = 0;
        }
        private void UpdateBehaviour() {
        
            if (_currentScore < _currentScoreAsset.value) {
                StartCoroutine(SetScoreCoroutine(_currentScoreAsset.value));
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

