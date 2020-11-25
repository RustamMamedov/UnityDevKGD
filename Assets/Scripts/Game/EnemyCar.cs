using Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class EnemyCar : Car {

        [SerializeField]
        private EventDispatcher _carCollisionDispatcher;

         [SerializeField]
        private EventDispatcher _carDodgedEventDispatcher;

        [SerializeField]
        private ScriptableIntValue _dodgedScore;

        [SerializeField]
        private ScriptableStringValue _dodgedCarName;

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                _carCollisionDispatcher.Dispatch();
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.CompareTag("PlayerDodge")) {
                _dodgedScore.value = carSettings.dodgeScore;
                _dodgedCarName.value = carSettings.name;
                _carDodgedEventDispatcher.Dispatch();
            }
        }
    }

}

