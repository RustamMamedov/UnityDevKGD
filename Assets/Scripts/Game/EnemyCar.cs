﻿using Events;
using UnityEngine;

namespace Game {

    public class EnemyCar : Car {

        [SerializeField]
        private EventDispatcher _collisionTrigger;

        [SerializeField]
        private EventDispatcher _carDodgedEventDispatcher;

        [SerializeField]
        private ScriptableIntValue _dodgedScore;

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                _collisionTrigger.Dispatch();
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.CompareTag("PlayerDodge")) {
                _dodgedScore.value = _carSettings.dodgeScore;
                _carDodgedEventDispatcher.Dispatch();
            }
        }
    }
}

