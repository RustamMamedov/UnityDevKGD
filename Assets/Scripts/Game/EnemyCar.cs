using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

namespace Game {
    public class EnemyCar : Car{
        [SerializeField]
        private EventDispatcher _enemyCarTriggerEventDispatcher;

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                Debug.Log("CarCollision");
                _enemyCarTriggerEventDispatcher.Dispatch();
            }
        }
    }
}
