using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Audio;

namespace Game {

    public class EnemyCar : Car {

        [SerializeField]
        private EventDispatcher _carTriggerEventDispatcher;

        [SerializeField]
        private AudioSourcePlayer _collisionSound;

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {              
                Debug.Log("CarCollision");
                _collisionSound.Play();
                _carTriggerEventDispatcher.Dispatch();
            }
        }
    }
}
