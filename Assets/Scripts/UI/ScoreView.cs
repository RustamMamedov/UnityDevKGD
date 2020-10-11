using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        private IEnumerator SetScoreCoroutine(int score) {

            while (score > _currentScore) {
                _currentScore++;
                yield return new WaitForSeconds(0.1f);

            }

            _isActiveSetScoreCoroutine = false;

            yield return null;

        }

        public void UpdateBehaviour() {

            if (_scriptableIntValue.value > _currentScore && !_isActiveSetScoreCoroutine) {
                _isActiveSetScoreCoroutine = true;
                StartCoroutine(SetScoreCoroutine(_scriptableIntValue.value));
            }
            Debug.Log(_currentScore);

        }

    }

}


