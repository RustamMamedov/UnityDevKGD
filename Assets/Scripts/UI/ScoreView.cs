using Events;
using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI { 
    public class ScoreView : MonoBehaviour
    {
            [SerializeField]
            private EventListener _update; //ссылка на отслеживатель событий

            private void Awake(){ //запуск, при рождении объекта
            _update.OnEventHappened += UpdateBehaviour; 
            }

            [SerializeField]
            private ScriptableIntValue CurrentScore; //отслеживает текущий счет

            [SerializeField]
            private int _currentScore; //счет, который выводится

            private IEnumerator SetScoreCoroutine(int score) { //корутина
            while (_currentScore < score) {
                _currentScore += 1;
                yield return new WaitForSeconds(0.1f);
            }
            }

            private void UpdateBehaviour() {
                if (_currentScore < CurrentScore.value) {
                    StartCoroutine(SetScoreCoroutine(CurrentScore.value));
                }
            Debug.Log(_currentScore);
            }

    }
}