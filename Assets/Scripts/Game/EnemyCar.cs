using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

namespace Game {

    public class EnemyCar : Car {

        [SerializeField]
        private EventDispatcher _enemyCarsCollisionEventDispatcher;

        [SerializeField]
        private ScriptableFloatValue _positionPlayerCarZ;

        [SerializeField]
        private float _distansCarZ;

        [SerializeField]
        private EventDispatcher _carDodgedEventDispatcher; 

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
            var distance = _positionPlayerCarZ.Value - transform.position.z;
            if (distance > _distansCarZ) {
                _carDodgedEventDispatcher.Dispatch();
            }
        }
    }
}
