using UnityEngine;
using Events;
using UI;

namespace Game {

    public class EnemyCar : Car {

        [SerializeField] private EventDispatcher _carCollisionEventDispatcher;

        [SerializeField] private ScriptableIntValue _currentScore;

        [SerializeField] private CarSettings _dodgeScore;
        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                _currentScore.value = 0;
                _carCollisionEventDispatcher.Dispatch();
                Debug.Log("CarCollision");
              

            }

            if (other.CompareTag("ScoreTrigger")) {
                _currentScore.value += _dodgeScore.dodgeScore;
            }
        }
    }

}