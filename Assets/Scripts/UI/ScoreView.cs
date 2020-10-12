using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



namespace UI {



    public class ScoreView:MonoBehaviour {
        private int _currentScore;

        [SerializeField]
        private CurrentScore _Scores;
        
        public void Update() {
        UpdateScore();
            
        }
        private IEnumerator UpdateScore() {
            yield return new WaitForSeconds(0.1f);
            if(_currentScore > _Scores.save_score) {
                _currentScore += 1;
            }

        }
    }

}