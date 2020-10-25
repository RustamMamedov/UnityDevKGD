﻿using System;
using Events;
using UnityEngine;

namespace Game {

    public class Car : MonoBehaviour {
        
#region Fields
        [SerializeField] 
        protected CarSettings _carSettings;
        
        [SerializeField]
        private EventListener _updateEventListener;
        
        [SerializeField]
        private EventListener _carCollisionEventListener;

        protected float _currentSpeed;
        
#endregion
        
        
#region LifeCycle
        protected void OnEnable() {
            SubscribeToEvents();
        }

        protected void OnDisable() {
            UnsubscribeToEvents();
        }
        
#endregion
        
#region Methods
        private void SubscribeToEvents() {
            _updateEventListener.OnEventHappened += UpdateBehaviour;
            _carCollisionEventListener.OnEventHappened += OnCarCollision;
        }
        
        private void UnsubscribeToEvents() {
            _updateEventListener.OnEventHappened -= UpdateBehaviour;
            _carCollisionEventListener.OnEventHappened -= OnCarCollision;
        }
        
        private void OnCarCollision() {
            //UnsubscribeToEvents();
        }
        
        private void UpdateBehaviour() {
            Move();
        }

        protected virtual void Move() {
            if (_currentSpeed < _carSettings.maxSpeed) {
                _currentSpeed += _carSettings.acceleration;
            }
            transform.Translate(transform.forward * (_currentSpeed * Time.deltaTime), Space.World);
        }
        
#endregion
    }
}