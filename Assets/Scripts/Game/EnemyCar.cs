using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;

namespace Game {

    public class EnemyCar : Car {
        [SerializeField]
        private EventDispatcher _carTriggerEventDispatcher;

        public static float EnemyPositionZ;

        protected override void Move() {
            base.Move();
            EnemyPositionZ = transform.position.z;
        }

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                _carTriggerEventDispatcher.Dispatch();
            }
        }
    }
}