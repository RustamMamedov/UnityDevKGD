using Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Values;

namespace UI {

    public class ScoreView : MonoBehaviour {

        // Fields.

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private ScriptableIntValue _currentScoreSource;

        [Tooltip("Time between displayed score updates.")]
        [SerializeField]
        private float _currentScoreIncrementDelay = 0.1f;

        // Displayed score.
        private int _currentScore;

        // True if score is currently updated.
        private bool _isBeingUpdated;


        // Life cycle.

        private void Start() {
            _updateEventListener.OnEventHappened += UpdateBehaviour;
        }

        private void UpdateBehaviour() {
            var targetScore = _currentScoreSource.score;
            if (_currentScore != targetScore) {
                StartCoroutine(SetScoreCoroutine(targetScore));
            }
            Debug.Log(_currentScore);
        }


        // Coroutines.

        // Update score by one toward target every _currentScoreIncrementDelay seconds.
        private IEnumerator SetScoreCoroutine(int targetScore) {
            while (_currentScore != targetScore) {
                if (_currentScore < targetScore) {
                    _currentScore--;
                } else {
                    _currentScore++;
                }
                yield return new WaitForSeconds(_currentScoreIncrementDelay);
            }
        }


    }

}

