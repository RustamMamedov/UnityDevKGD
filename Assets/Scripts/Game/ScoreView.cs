using Events;
using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField]
        private ScriptableIntValue currentScore;//Не уверен что приватное, протребуется ли нам менять его во время игры?!
        [SerializeField]
        private int _currentScore;
        [SerializeField]
        private EventListener _update;

        private void Awake()
        {
            _update.OnEventHappened += UpdateBehaviour;
        }

        private void UpdateBehaviour()
        {
            if (currentScore.value > _currentScore)
                StartCoroutine(SetScoreCoroutine(currentScore.value));
            Debug.Log(_currentScore);
        }

        private IEnumerator SetScoreCoroutine(int score)
        {
            while(_currentScore<score)
            {
                _currentScore++;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
