using Events;
using UnityEngine;

namespace Game {

    public class Car : MonoBehaviour {

        [SerializeField]
        protected CarSettings _carSettings;
        public CarSettings CarSettings { get { return _carSettings; } }


        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private EventListener _carCollisionEventListener;

        protected float _carrentSpeed;

        private void OnEnable() {

            SubscribeToEvents();
        }

        private void OnDisable() {

            UnsubscribeToEvents();
        }

        protected virtual void SubscribeToEvents() {

            _updateEventListener.OnEventHappened += UpdateBehaviour;
            _carCollisionEventListener.OnEventHappened += OnCarCollision;
        }

        protected virtual void UnsubscribeToEvents() {

            _updateEventListener.OnEventHappened -= UpdateBehaviour;
            _carCollisionEventListener.OnEventHappened -= OnCarCollision;
        }

        private void OnCarCollision() {

            UnsubscribeToEvents();
        }

        private void UpdateBehaviour() {

            Move();
        }

        protected virtual void Move() {
            
            if (_carrentSpeed < _carSettings.maxSpeed) {

                _carrentSpeed += _carSettings.acceleration;
            }
            transform.Translate(transform.forward * _carrentSpeed * Time.deltaTime, Space.World);
        }
    }
}

