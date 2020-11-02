using UnityEngine;
using Events;

namespace Game {
    public class EnemyCar : Car {

        [SerializeField]
        private EventDispatcher _carCollisionEventDispatcher;

        [SerializeField]
        private ScriptableIntValue _currentScoreValue;

        [SerializeField]
        private ScriptableFloatValue _playerPositionZ;

        protected override void SubscribeToEvents() {
            base.SubscribeToEvents();
            _updateEventListener.OnEventHappened += CheckAndHandlePlayerDodge;
        }

        protected override void UnsubscribeToEvents() {
            base.UnsubscribeToEvents();
            _updateEventListener.OnEventHappened -= CheckAndHandlePlayerDodge;
        }

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                _carCollisionEventDispatcher.Dispatch();
            }
        }

        private void CheckAndHandlePlayerDodge() {
            if (_playerPositionZ.value > transform.position.z) {
                _updateEventListener.OnEventHappened -= CheckAndHandlePlayerDodge;
                _currentScoreValue.value += _carSettings.dodgeScore;
            }
        }
    }
}