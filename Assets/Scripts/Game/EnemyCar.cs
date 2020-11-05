using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

namespace Game {
    public class EnemyCar : Car {
        [SerializeField]
        private EventDispatcher _enemyCarsCollisionEventDispatcher;

        [SerializeField]
        private EventDispatcher _carDodged;

        [SerializeField]
        private ScriptableFloatValue _distanceBetweenCars;

        [SerializeField]
        private ScriptableFloatValue _playerPosition;


        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                _enemyCarsCollisionEventDispatcher.Dispatch();
            }
        }

        protected override void Move() {
            base.Move();
            Dodged();
        }

        private void Dodged() {
            if(_playerPosition.value - transform.position.z > _distanceBetweenCars.value) {
                _carDodged.Dispatch();
            }
        }
    }
}