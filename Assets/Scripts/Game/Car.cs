using Events;
using UnityEngine;

namespace Game {

    public class Car : MonoBehaviour {

        [SerializeField]
        private CarSettings _carSettings;

        [SerializeField]
        private EventListener _carCollisionEventListener;

        [SerializeField]
        private EventListener _updateEventListener;

        protected float _currentSpeed;


        protected virtual void OnEnable() {
            SubscribeToEvents();
        }

        protected virtual void OnDisable() {
            UnsubscribeToEvents();
        }

        private void UpdateBehaviour() {
            Move();
        }

        private void SubscribeToEvents() {
            _updateEventListener.OnEventHappened += UpdateBehaviour;
            _carCollisionEventListener.OnEventHappened += OnCarCollision;
        }

        private void UnsubscribeToEvents() {
            _updateEventListener.OnEventHappened -= UpdateBehaviour;
            _carCollisionEventListener.OnEventHappened -= OnCarCollision;
        }

        private void OnCarCollision() {
            UnsubscribeToEvents();
        }

        protected virtual void Move() {
            if (_currentSpeed < _carSettings.maxSpeed) {
                _currentSpeed += _carSettings.acceleration;
            }
            transform.Translate(transform.forward * _currentSpeed * Time.deltaTime, Space.World);
        }


    }
}