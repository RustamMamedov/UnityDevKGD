using Events;
using UnityEngine;

namespace Game {

    public class Car : MonoBehaviour {

        [SerializeField] private CarSettings _carSettings;

        [SerializeField] private EventListener _updateEventListener;

        [SerializeField] private EventListener _carCollisionEventListener;

        protected float _currentSpeed;


        protected virtual void OnEnable() {
            SubscribeToEvent();
        }

        protected virtual void OnDisable() {
            UnsubscribeToEvent();
        }


        private void SubscribeToEvent() {
            _updateEventListener.OnEventHappened += UpdateBehaviour;
            _carCollisionEventListener.OnEventHappened += OnCarCollision;
        }

        private void UnsubscribeToEvent() {
            _updateEventListener.OnEventHappened -= UpdateBehaviour;
            _carCollisionEventListener.OnEventHappened -= OnCarCollision;
        }

        private void OnCarCollision() {
            UnsubscribeToEvent();
        }

        private void UpdateBehaviour() {
            Move();
        }

        protected virtual void Move() {
            Debug.Log("MOVE");
            if (_currentSpeed < _carSettings.maxSpeed) {
                _currentSpeed += _carSettings.acceleration;
            }

            transform.Translate(transform.forward * _currentSpeed * Time.deltaTime, Space.World);
        }


    }
}