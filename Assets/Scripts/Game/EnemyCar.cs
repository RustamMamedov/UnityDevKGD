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
        private ScriptableFloatValue _distansBetweenCarsZ;

        [SerializeField]
        private EventDispatcher _carDodgedEventDispatcher;

        [SerializeField]
        private ScriptableIntValue _currentDodged;

        private bool _dodged=false;

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
            if (distance > _distansBetweenCarsZ.Value) {
                _carDodgedEventDispatcher.Dispatch();
                if (!_dodged) {
                    _currentDodged.Value++;
                    _dodged = true;
                }
            }
        }
    }
}
