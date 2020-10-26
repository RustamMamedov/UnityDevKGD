using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using Events;

namespace Game {
    public class EnemyCar : Car {
        [SerializeField]
        private EventDispatcher _carTrigerEventDispatcher;

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                _carTrigerEventDispatcher.Dispatch();
                Debug.Log("CarCollision with " + transform.name);
            }
        }
        protected override void Move() {
          
        }
    }
}

