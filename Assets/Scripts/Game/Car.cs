﻿using Events;
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

        protected virtual void OnEnable(){
            SubscribeToEvents();
        }

        protected virtual void OnDisable(){
            UnsubscribeToEvents();
        }

        private void SubscribeToEvents(){
            _updateEventListener.OnEventHappened += UpdateBehaviour;
            _carCollisionEventListener.OnEventHappened += OnCarCollision;
        }

        protected virtual void UnsubscribeToEvents(){
            _updateEventListener.OnEventHappened -= UpdateBehaviour;
            _carCollisionEventListener.OnEventHappened -= OnCarCollision;
        }

        private void UpdateBehaviour(){
            UnsubscribeToEvents();
        }

        private void OnCarCollision(){
            Move();
        }

        private void Awake() {
            _updateEventListener.OnEventHappened += Move;
        }

        private void Move() {
            if (_currentSpeed < _carSettings.maxSpeed){
                _currentSpeed += _carSettings.acceleration;
            }
            transform.Translate(transform.forward * _currentSpeed * Time.deltaTime, Space.World);
        }
    }
}