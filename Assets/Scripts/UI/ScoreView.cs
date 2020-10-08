using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI { 
    public class ScoreView : MonoBehaviour
    {
            [SerializeField]
            private ScriptableIntValue CurrentScore; //отслеживает текущий счет

            [SerializeField]
            private int _currentScore; //счет, который выводится

            private IEnumerator SetScoreCoroutine() { 
                
            }

            private void UpdateBehaviour() {
                if (_currentScore < CurrentScore.value) {
                    StartCoroutine(SetScoreCoroutine());
                }
            }

    }
}