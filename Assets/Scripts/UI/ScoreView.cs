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

        // Number of the current SetScoreCoroutine.
        private int _currentSetScoreCoroutine = 0;

        // To what value is current score being changed at the moment.
        private int _currentTargetScore;


        // Life cycle.

        private void Start() {
            _currentScore = _currentScoreSource.value;
            _currentTargetScore = _currentScore;
            _updateEventListener.OnEventHappened += UpdateBehaviour;
        }

        private void UpdateBehaviour() {
            var targetScore = _currentScoreSource.value;
            if (_currentTargetScore != targetScore) {
                StartCoroutine(SetScoreCoroutine(targetScore));
            }
            Debug.Log(_currentScore);
        }


        // Coroutines.

        // Update score by one toward target every _currentScoreIncrementDelay seconds.
        private IEnumerator SetScoreCoroutine(int targetScore) {

            // Make number and set it as current coroutine number.
            // If current coroutine number is changed, this coroutine must stop.
            // As a result, no more than one coroutine will change _currentScore
            // at any given frame (unless several are called simultaneously).
            int coroutineNumber = _currentSetScoreCoroutine + 1;
            _currentSetScoreCoroutine = coroutineNumber;
            _currentTargetScore = targetScore;

            // Update current score.
            int scoreChange = _currentScore < targetScore ? 1 : -1;
            while (true) {
                if (_currentSetScoreCoroutine != coroutineNumber || _currentScore == targetScore) {
                    break;
                }
                _currentScore += scoreChange;
                yield return new WaitForSeconds(_currentScoreIncrementDelay);
            }

        }


    }

}

