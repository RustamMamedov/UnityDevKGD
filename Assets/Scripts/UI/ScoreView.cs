using Events;
using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
    public class ScoreView : MonoBehaviour {

        [SerializeField]
        private ScriptableIntValue _currentScoreAsset;

        [SerializeField]
        private EventListener _eventListener;
        
        private int _currentScore = 0;
        private bool _isScoreChanging = false;

        private void Awake() {
            _eventListener.OnEventHappened += UpdateBehaviour;
        }

        public void UpdateBehaviour() {
            if (_currentScoreAsset.value > _currentScore && !_isScoreChanging) {
                _isScoreChanging = true;
                StartCoroutine(SetScoreCoroutine(_currentScoreAsset.value));
            }
        }

        private IEnumerator SetScoreCoroutine(int targetScore) {
            while (_currentScore != targetScore) {
                _currentScore++;
                Debug.Log(_currentScore);

                yield return new WaitForSeconds(0.1f);
            }
            _isScoreChanging = false;
        }
    }
}

