using UnityEngine;
using Events;

namespace Game {
    public class EnemyCar : Car {
        [SerializeField]
        private EventDispatcher _carCollisionEventDispatcher;

        [SerializeField]
        private ScriptableIntValue _currentScore;

        [SerializeField]
        private ScriptableFloatValue _playerPositionZ;

        private bool _dodged = false;

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                _carCollisionEventDispatcher.Dispatch();
                Debug.Log("CarCollision with " + transform.name);
            }
        }

        protected override void UpdateBehaviour() {
            if (_playerPositionZ.value >= gameObject.transform.position.z && !_dodged) {
                _dodged = true;
                _currentScore.value += _carSettings.dodgeScore;
            }
            base.UpdateBehaviour();
        }
    }
}

