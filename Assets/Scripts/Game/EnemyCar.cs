using System.Collections;
using UnityEngine;
using Events;
using Audio;

namespace Game {

    public class EnemyCar : Car {

        [SerializeField]
        private EventDispatcher _carTriggerEventDispatcher;

        [SerializeField]
        private AudioSource _collisionSound;

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                Save.Instance.StartSaveProcess();
                CollisionSound();
                _carTriggerEventDispatcher.Dispatch();
            }
        }

        private void CollisionSound() {
            _collisionSound.Play();
        }

    }
}
