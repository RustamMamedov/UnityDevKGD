using Events;
using UnityEngine;

namespace Game {
    public class EnemyCar : Car {

        [SerializeField]
        private EventDispatcher _collisionTrigger;

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                _collisionTrigger.Dispatch();
                Debug.Log("trigger");
            }
        }
    }
}

