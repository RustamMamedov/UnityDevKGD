using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events; 
using Game;
using UnityEngine.UI;

 
namespace UI { 
 
    public class ScoreView : MonoBehaviour {

        [SerializeField]
        private ScriptableIntValue _currentScoreAsset;

        private int _currentScore;

        [SerializeField]
        private EventListener _eventListener;

        [SerializeField]
        private float _scoreCountDelay;

        [SerializeField]
        private Text _scoreLabel;


        private void Awake() {
            _eventListener.OnEventHappend += UpdateBehaviour;
        }

        private void OnEnable() {
            _currentScoreAsset.value = 0;
            _scoreLabel.text = "0";
            _currentScore = 0;

        }

        private void UpdateBehaviour() {
            if (_currentScoreAsset.value > _currentScore)
                StartCoroutine(SetScoreCoroutine(_currentScoreAsset.value));
            
        }

        private IEnumerator SetScoreCoroutine(int score) {
            while (_currentScore < score) {
                _currentScore++;
                _scoreLabel.text = $"{_currentScore}";
                yield return new WaitForSeconds(_scoreCountDelay);
            }
        }
    }
}