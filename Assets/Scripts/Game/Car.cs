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

        protected float _currentSpeed;

        private void OnEnable() {
            SubcribeToEvents();
        }

        private void OnDisable() {
            UnsubscribeToEvents();
        }

        public int GetCarDodgeScore() {
            return _carSettings.dodgeScore;
        }

        protected virtual void SubcribeToEvents() {
            _updateEventListener.OnEventHappened += UpdateBehavior;
            _carCollisionEventListener.OnEventHappened += OnCarCollision;
        }

        protected virtual void UnsubscribeToEvents() {
            _updateEventListener.OnEventHappened -= UpdateBehavior;
            _carCollisionEventListener.OnEventHappened -= OnCarCollision;
        }

        protected virtual void OnCarCollision() {
            UnsubscribeToEvents();
        }

        private void UpdateBehavior() {
            Move();
        }

        protected virtual void Move() {
            if (_currentSpeed < _carSettings.maxSpeed) {
                _currentSpeed += _carSettings.acceleration;
            }
            transform.Translate(transform.forward * _currentSpeed * Time.deltaTime, Space.World);
        }
    }
}