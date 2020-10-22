using UnityEngine;
using Events;

namespace Game {
    public class Car : MonoBehaviour {

        [SerializeField]
        protected CarSettings _carSettings;

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private EventListener _carCollisionEventListener;

        protected float _currentSpeed;
 

        private void Awake() {
            _updateEventListener.OnEventHappened += Move;
            _updateEventListener.OnEventHappened += Die;
        }

        protected virtual void OnEnable() {
            SubScribeToEvents();
        }

        protected virtual void OnDisable() {
            UnSubscribeToEvents();
        }

        private void SubScribeToEvents() {
            _updateEventListener.OnEventHappened += UpdateBehaviour;
            _carCollisionEventListener.OnEventHappened += OnCarCollision;
        }

        private void UnSubscribeToEvents() {
            _updateEventListener.OnEventHappened -= UpdateBehaviour;
            _carCollisionEventListener.OnEventHappened -= OnCarCollision;
        }

        private void OnCarCollision() {
            UnSubscribeToEvents();
        }

        private void UpdateBehaviour() {
            Move();
        }

        protected virtual void Move() {
            if(_currentSpeed < _carSettings.maxSpeed) {
                _currentSpeed += _carSettings.acceleration;
            }
            transform.Translate(transform.forward * _currentSpeed * Time.deltaTime, Space.World);
            
        }

        private void Die() {
            
        }

    }
}

