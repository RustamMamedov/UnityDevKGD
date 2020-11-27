using UnityEngine;
using Events;

namespace Game {

    public class EnemyCar : Car {

        [SerializeField]
        private EventDispatcher _carCollisionEventDispatcher;

        [SerializeField]
        private EventDispatcher _carDodgeEventDispatcher;

        [SerializeField]
        private ScriptableIntValue _dodgeScore;

        [SerializeField]
        private ScriptableStringValue _dodgedCarName;

        private void OnTriggerEnter(Collider other) {
            
            if (other.CompareTag("Player")) {

                _carCollisionEventDispatcher.Dispatch();
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.CompareTag("PlayerDodge")) {
                _dodgeScore.value = _carSettings.dodgeScore;
                _dodgedCarName.name = _carSettings.name;
                _carDodgeEventDispatcher.Dispatch();
            }
        }
    }
}

