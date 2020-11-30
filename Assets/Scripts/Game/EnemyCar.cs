using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using UI;

namespace Game {

    public class EnemyCar : Car {

        [SerializeField]
        private EventDispatcher _carCollisionEventDispatcher;

        [SerializeField]
        private EventDispatcher _carDodgedEventDispatcher;

        [SerializeField]
        private ScriptableIntValue _dodgeScore;

        [SerializeField]
        private ScriptableStringValue _dodgeCarName;

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                _carCollisionEventDispatcher.Dispatch();
                Debug.Log("Crashed with: " + transform.name);
            }

            if (other.CompareTag("PlayerDodge")) {
                _dodgeScore.value = _carSettings.dodgeScore;
                _dodgeCarName.value = _carSettings.name;
                _carDodgedEventDispatcher.Dispatch();
            }
        }
    }
}
