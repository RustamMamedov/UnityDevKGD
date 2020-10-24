using System;
using System.Collections;
using System.Collections.Generic;
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
        private void Awake() {
            _updateEventListener.OnEventHappened += Move;
        }
        protected virtual void Move() {
            if (_currentSpeed < _carSettings.maxSpeed) {
                _currentSpeed += _carSettings.acceleration;
            }
            transform.Translate(transform.forward * _currentSpeed * Time.deltaTime, Space.World);
        }

        private void SubscribeToEvent() {
            _updateEventListener.OnEventHappened += UpdateBehaviour;
            _carCollisionEventListener.OnEventHappened += OnCarCollision;
        }
        
        private void UnSubscribeToEvent() {
            _updateEventListener.OnEventHappened -= UpdateBehaviour;
            _carCollisionEventListener.OnEventHappened -= OnCarCollision;
        }

        private void OnCarCollision() {
            UnSubscribeToEvent();
        }

        private void UpdateBehaviour() {
            Move();
        }
    }
}