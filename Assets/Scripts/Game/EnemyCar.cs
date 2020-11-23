using Events;
using UnityEngine;
using Values;

namespace Game {
    
    public class EnemyCar : Car {

        // Fields.

        [SerializeField]
        private EventDispatcher _carCollisionDispatcher;

        [SerializeField]
        private EventDispatcher _carDodgedDispatcher;

        [SerializeField]
        private ScriptableCarSettingsReference _dodgedCarReference;

        [SerializeField]
        private ScriptableFloatValue _playerPositionZValue;

        // True if dodge already happened.
        private bool _dodged = false;


        // Life cycle.

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                _carCollisionDispatcher.Dispatch();
            }
        }


        // Motion methods.

        protected override void Move() {
            base.Move();
            if (!_dodged && transform.position.z < _playerPositionZValue.value - CarSettings.dodgeDistance) {
                _dodged = true;
                _dodgedCarReference.reference = CarSettings;
                _carDodgedDispatcher.Dispatch();
            }
        }


    }

}
