using Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class Car : MonoBehaviour {

        // Fields.

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private EventListener _carCollisionListener;

        [SerializeField]
        private CarSettings _carSettings;

        private float _currentSpeed;


        // Properties.

        public CarSettings CarSettings => _carSettings;
        public float CurrentSpeed => _currentSpeed;


        // Life cycle.

        private void OnEnable() {
            SubscribeToEvents();
        }

        private void OnDisable() {
            UnsubscribeFromEvents();
        }


        // Event handling.

        protected virtual void SubscribeToEvents() {
            _updateEventListener.OnEventHappened += UpdateBehaviour;
            _carCollisionListener.OnEventHappened += OnCarCollision;
        }

        protected virtual void UnsubscribeFromEvents() {
            _updateEventListener.OnEventHappened -= UpdateBehaviour;
            _carCollisionListener.OnEventHappened -= OnCarCollision;
        }

        private void UpdateBehaviour() {
            Move();
        }

        private void OnCarCollision() {
            UnsubscribeFromEvents();
        }


        // Supportive methods.

        protected virtual void Move() {
            _currentSpeed += _carSettings.acceleration * Time.deltaTime;
            _currentSpeed = Mathf.Min(_currentSpeed, _carSettings.maxSpeed);
            transform.Translate(transform.forward * _currentSpeed * Time.deltaTime, Space.World);
        }


    }

}