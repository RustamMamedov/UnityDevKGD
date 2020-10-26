﻿using UnityEngine;
using Events;

namespace Game {

    public class EnemyCar : Car {

        [SerializeField]
        private EventDispatcher _carCollisionEventDispatcher;

        private void OnTriggerEnter(Collider other) {
            
            if (other.CompareTag("Player")) {

                _carCollisionEventDispatcher.Dispatch();
                Debug.Log("CarCollision");
            }
        }
    }
}

