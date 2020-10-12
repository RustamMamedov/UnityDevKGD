using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using Events;

namespace UI {

    public class ScoreView : MonoBehaviour {
        [SerializeField] 
        private ScriptableIntValue _scoreLink;
        [SerializeField]
        private EventListener _update;

        private int _currentScore = 0;
        
        private void Awake() {
            _update.OnEventHappened += UpdateBehaviour;
        }

        private void UpdateBehaviour() {
            if (_scoreLink.value > _currentScore) {
                StartCoroutine(SetScoreCoroutine(_scoreLink.value));
            }
        }

        private IEnumerator SetScoreCoroutine(int score) {
            while (_currentScore != score) {
                _currentScore ++;
                Debug.Log(_currentScore);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}