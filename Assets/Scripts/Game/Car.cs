using Events;
using UnityEngine;

namespace Game {
    public class Car : MonoBehaviour {

        [SerializeField]
        protected CarSettings _carSettings;

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private EventListener _carCollisionEventListener;

        private float _currentSpeed;

        protected virtual void OnEnable() {
            SubcribeToEvents();
        }

        protected virtual void OnDisable() {
            UnsubcribeToEvents();
        }

        private void SubcribeToEvents() {
            _updateEventListener.OnEventHappened += UpdateBehavior;
            _carCollisionEventListener.OnEventHappened += UpdateBehavior;
        }

        private void UnsubcribeToEvents() {
            _updateEventListener.OnEventHappened -= UpdateBehavior;
            _carCollisionEventListener.OnEventHappened -= UpdateBehavior;
        }

        private void OnCarCollision() {
            UnsubcribeToEvents();
        }

        private void UpdateBehavior() {
            Move();
        }

        protected virtual void Move() {
            if(_currentSpeed < _carSettings.maxSpeed) {
                _currentSpeed += _carSettings.acceleration;
            }
            transform.Translate(transform.forward * _currentSpeed * Time.deltaTime, Space.World);
        }
    }
}