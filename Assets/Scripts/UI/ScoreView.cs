using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events; 
using Game; 

 
namespace UI { 
 
    public class ScoreView : MonoBehaviour {

        [SerializeField]
        private ScriptableIntValue _currentScoreAsset;

        private int _currentScore;

        [SerializeField]
        private EventListener _eventListener;


        private void Awake() {
            _eventListener.OnEventHappend += UpdateBehaviour;
        }

        private void UpdateBehaviour() {
            if (_currentScoreAsset.value > _currentScore)
                StartCoroutine(SetScoreCoroutine(_currentScoreAsset.value));
            
        }

        private IEnumerator SetScoreCoroutine(int score) {
            while (_currentScore < score) {
                _currentScore++;
                Debug.Log(_currentScore);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}