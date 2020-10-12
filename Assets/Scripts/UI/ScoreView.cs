using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using Events;
using UnityEditor;

namespace UI
{
    public class ScoreView : MonoBehaviour
    {
        private int _currentScore;

        [SerializeField]

        ScriptableIntValue CurrentScore;

        [SerializeField]

        EventListener _eventListener;

        public void UpdateBehavior()
        {
            if (CurrentScore.score > _currentScore)
            {
                StartCoroutine(SetScoreCoroutine(CurrentScore.score));
            }
        }

        private IEnumerator SetScoreCoroutine (int score)
        {
            while (_currentScore != score)
            {
                _currentScore++;
                Debug.Log(_currentScore);
                yield return new WaitForSeconds(1f);
            }
        }

    }

}
