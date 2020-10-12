using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace UI {
    public class ScoreView : MonoBehaviour {
        [SerializeField]
        private int _currentScore;
        [SerializeField]
        private SciptableIntValue _Scores;



        public void UpdateBehaviour() {
            if (_currentScore > _Scores.save_score) {
                StartCoroutine(UpdateScore());
            }
            
            
        }
        private IEnumerator UpdateScore() {
            yield return new WaitForSeconds(0.1f);
            if (_currentScore > _Scores.save_score) {
                _currentScore += 1;
            }
        }
    }
}