using System.Collections;
using UnityEngine;
using Game;
using Events;

namespace UI {

    public class ScoreView : MonoBehaviour {
    //Fields//
        [SerializeField] 
        private ScriptableIntValue _currentScoreAsset;
        
        private int _currentScore = 0;
        
        [SerializeField] 
        private EventListener _eventListener;
        
    //Methods//
        private void Awake() {
            _eventListener.OnEventHappened += UpdateBehaviour;
        }

        private void UpdateBehaviour() {
            if (_currentScoreAsset.score > _currentScore) StartCoroutine(SetScoreCoroutine(_currentScoreAsset));
        }
    //Coroutine//
        private IEnumerator SetScoreCoroutine(ScriptableIntValue _currentScoreAsset) {
            while (_currentScore < _currentScoreAsset.score) {
                _currentScore++;
                Debug.Log("Score: " + _currentScore + " TargetScore: " + _currentScoreAsset.score);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}

