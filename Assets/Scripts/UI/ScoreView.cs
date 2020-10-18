using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Game;
using Events;
namespace UI {

    public class ScoreView : MonoBehaviour {

        [SerializeField]
        private float _scoreCountDelay;

        [SerializeField]
        private Text _scoreLabel;

        [SerializeField]
        private ScriptableIntValue currentScore;

        [SerializeField]
        private EventListener eventListener;

        private int _currentScore;

        private bool isScoreChanging = false;

        

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
            while (_currentScore < newScore) {
                _currentScore++;
                _scoreLabel.text = $"{_currentScore}";
                Debug.Log(_currentScore);
                yield return new WaitForSeconds(_scoreCountDelay);
            }
            isScoreChanging = false;
        }
    }

}