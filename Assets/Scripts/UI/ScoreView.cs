using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Game;

namespace UI
{
    public class ScoreView : MonoBehaviour
    {
        private void Awake()
        {
            _update.OnEventHappened += UpdateBehaviour;
        }
        [SerializeField]
        private ScriptableIntValue CurrentScore;
        [SerializeField]
        private int _currentScore;
        [SerializeField]
        private EventListener _update;
        public void UpdateBehaviour()
        {
            if (_currentScore != CurrentScore.value)
            {
                StartCoroutine(SetScoreCoroutine());
            }
        }
        public IEnumerator SetScoreCoroutine()
        {
            while (_currentScore < CurrentScore.value)
            {
                _currentScore++;
                Debug.Log(_currentScore);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
