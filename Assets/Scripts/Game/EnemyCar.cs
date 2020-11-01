using Events;
using UnityEngine;
using Values;

namespace Game {
    
    public class EnemyCar : Car {

        // Fields.

        // Minimal distance along Z axis between this and player cars
        // needed for dodge to happen.
        [SerializeField]
        private float _dodgeDistance;

        [SerializeField]
        private EventDispatcher _carCollisionDispatcher;

        [SerializeField]
        private EventDispatcher _carDodgedDispatcher;

        [SerializeField]
        private ScriptableIntValue _currentScoreValue;

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
            if (!_dodged && transform.position.z < _playerPositionZValue.value - _dodgeDistance) {
                _dodged = true;
                _currentScoreValue.value += CarSettings.dodgeScore;
                _carDodgedDispatcher.Dispatch();
            }
        }


    }

}
