using System.Collections;
using UnityEngine;
using Game;
using Events;

namespace UI {
    public class ScoreView : MonoBehaviour {
        [SerializeField]
        private int _currentScore;

        [SerializeField]
        private ScriptableIntValue _scriptableIntValue;

        [SerializeField]
        private EventListener _updateListener;

        [SerializeField]
        private bool _isActiveSetScoreCoroutine = false;

        private void Awake() {
            _updateListener.OnEventHappened += UpdateBehaviour;
        }

        private IEnumerator SetScoreCoroutine(int value) {
            while (value > _currentScore) {
                _currentScore++;
                yield return new WaitForSeconds(0.1f);
            }
            _isActiveSetScoreCoroutine = false;

            yield return null;
        }

        private void UpdateBehaviour() {
            if (_scriptableIntValue.value > _currentScore && !_isActiveSetScoreCoroutine) {
                _isActiveSetScoreCoroutine = true;
                StartCoroutine(SetScoreCoroutine(_scriptableIntValue.value));
            }
            Debug.Log(_currentScore);
        }
    }
}
