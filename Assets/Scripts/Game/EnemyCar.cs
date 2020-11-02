using UnityEngine;
using Events;
using UI;

namespace Game {

    public class EnemyCar : Car {

        [SerializeField] private EventDispatcher _carCollisionEventDispatcher;

        [SerializeField] private ScriptableIntValue _currentScore;

        [SerializeField] private CarSettings _dodgeScore;

        public CarSettings DodgeScore => _dodgeScore;
        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                _currentScore.value = 0;
                _carCollisionEventDispatcher.Dispatch();
                Debug.Log("CarCollision");
              

            }

        }
    }

}