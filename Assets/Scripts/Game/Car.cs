﻿using Events;
using UnityEngine;
using UI;
using Sirenix.OdinInspector;

namespace Game {

    public class Car : MonoBehaviour {

        [SerializeField]
        [Required]
        public CarSettings carSettings;

        [SerializeField]
        private EventListener _carCollisionEventListener;

        [SerializeField]
        private EventListener _updateEventListener;

        protected float _currentSpeed;

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
            if (_currentSpeed < carSettings.maxSpeed) {
                _currentSpeed += carSettings.acceleration;
            }
            transform.Translate(transform.forward * _currentSpeed * Time.deltaTime, Space.World);
        }
        [ContextMenu("IncreaseDodgeScore")]
        private void IncreaseDodgeScore() {
            carSettings.dodgeScore++;
        }
    }
}