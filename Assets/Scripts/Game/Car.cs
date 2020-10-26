using Events;
using UnityEngine;

namespace Game {

    public class Car : MonoBehaviour {

        [SerializeField]
        private CarSettings _carSettings;

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private EventListener _carCollisionEventListener;

        protected float _currentSpeed;

        private void OnEnable() {
            SubscribeToEvents();
        }

        private void OnDisable() {
            UnsubscribeToEvents();
        }

        protected virtual void Move() {
            if (_currentSpeed < _carSettings.maxSpeed) {
                _currentSpeed += _carSettings.acceleration;
            }
            transform.Translate(transform.forward * _currentSpeed * Time.deltaTime, Space.World);
        }

        protected virtual void UnsubscribeToEvents() {
            _updateEventListener.OnEventHappened -= UpdateBehaviour;
            _carCollisionEventListener.OnEventHappened -= OnCarCollision;
        }

        private void SubscribeToEvents() {
            _updateEventListener.OnEventHappened += UpdateBehaviour;
            _carCollisionEventListener.OnEventHappened += OnCarCollision;
        }

        private void OnCarCollision() {
            Debug.Log("CarCollision");
            // Commented for debug
            //UnsubscribeToEvents();
        }

        private void UpdateBehaviour() {
            Move();
        }

    }
}
