using Events;
using UnityEngine;

namespace Game {

    public class EnemyCar : Car {

        [SerializeField]
        private EventDispatcher _carTriggerEventDispatcher;

        protected override void OnEnable() {
            
        }

        protected override void OnDisable() {
            
        }

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                _carTriggerEventDispatcher.Dispatch();
                Debug.Log("CarCollision");
            }
        }
    }
}