using System.Collections;
using UnityEngine;
using Game;
using Events;

namespace UI {
    public class ScoreView : MonoBehaviour {

        [SerializeField]
        private ScriptableIntValue _currentScoreAsset;

        [SerializeField]
        private int _currentScore;

        [SerializeField]
        private EventListener _eventListener;

        [SerializeField]
        private float _currentScoreDelay = 0.1f;

        private void Awake() {
            _eventListener.OnEventHappened += UpdateBehaviour;
        }

        private void UpdateBehaviour() {
            var target = _currentScoreAsset._score;
            if (target > _currentScore) {
                Debug.Log(_currentScore + "->" + target);
                StartCoroutine(SetScoreCoroutine(target));
            }
        }

        private IEnumerator SetScoreCoroutine(int target) {

            while (_currentScore < target) {
                _currentScore++;
                Debug.Log(_currentScore + " -> " + target);
                yield return new WaitForSeconds(_currentScoreDelay);
            }

        }
    }

}


