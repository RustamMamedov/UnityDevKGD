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

        private void OnEnable() {
            SubScribeToEvents();
        }

        private void OnDisable() {
            UnSubScribeToEvents();
        }

        protected virtual void SubScribeToEvents() {
            _updateEventListener.OnEventHappened += UpdateBehaviour;
            _carCollisionEventListener.OnEventHappened += OnCarCollision;
        }

        protected virtual void UnSubScribeToEvents() {
            _updateEventListener.OnEventHappened -= UpdateBehaviour;
            _carCollisionEventListener.OnEventHappened -= OnCarCollision;
        }

        private void OnCarCollision() {
            UnSubScribeToEvents();
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

      

    }
}

