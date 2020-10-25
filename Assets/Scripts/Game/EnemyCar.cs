using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

namespace Game {
    public class EnemyCar : Car {
        [SerializeField]
        private EventDispatcher _enemyCarsCollisionEventDispatcher;

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                _enemyCarsCollisionEventDispatcher.Dispatch();
            }
        }
    }
}
