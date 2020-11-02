using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Events;
using Game;

namespace UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField]
        private Text _scores;

        [SerializeField]
        private ScriptableIntValue CurrentScore;
        
        [SerializeField]
        private EventListener _update;

        private int _currentScore;
        private void OnEnable()
        {
            CurrentScore.value = 0;
            _currentScore = 0;
            _scores.text = _currentScore.ToString();
            _update.OnEventHappened += UpdateBehaviour;
        }
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
                _scores.text = _currentScore.ToString();
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
