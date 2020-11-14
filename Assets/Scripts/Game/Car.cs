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
        
        [SerializeField] 
        private Color _gizmosColor = Color.red;
        
        [SerializeField]
        private List<Transform> _carLights = new List<Transform>();

        #if UNITY_EDITOR
        public CarSettings CarSettings => _carSettings;
        #endif

        protected float _currentSpeed;

        private void OnEnable() {
            SubscribeToEvents();
        }

        protected virtual void OnDisable() {
            UnsubscribeToEvents();
        }

        protected virtual void Move() {
            if (_currentSpeed < _carSettings.maxSpeed) {
                _currentSpeed += _carSettings.acceleration;
            }

            transform.Translate(transform.forward * _currentSpeed * Time.deltaTime, Space.World);
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

        private void OnDrawGizmos() {
            Gizmos.color = _gizmosColor;
            foreach (var carLight in _carLights) {
                Gizmos.DrawFrustum(carLight.position, 45f, 2f + _carSettings.lightLenght, 0f, 2f); 
            }
        } 

        [ContextMenu("IncreaseDodgeScore")]
        private void IncreaseDodgeScore() {
            _carSettings.dodgeScore++;
        }
    }
}