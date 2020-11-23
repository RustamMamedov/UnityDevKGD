using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

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
        private List<Light> _carLights = new List<Light>();

        #if UNITY_EDITOR
        public CarSettings CarSettings => _carSettings;
        #endif

        protected float _currentSpeed;

        private void OnEnable() {
            SubscribeToEvents();
            foreach (var carLight in _carLights) {
                carLight.range = _carSettings.lightLenght;
            }
        }

        protected virtual void OnDisable() {
            UnsubscribeFromEvents();
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

        protected virtual void UnsubscribeFromEvents() {
            _updateEventListener.OnEventHappened -= UpdateBehaviour;
            _carCollisionEventListener.OnEventHappened -= OnCarCollision;
        }

        protected virtual void OnCarCollision() {
            UnsubscribeFromEvents();
        }

        private void UpdateBehaviour() {
            Move();
        }

        private void OnDrawGizmos() {
            Gizmos.color = _gizmosColor;
            foreach (var carLight in _carLights) {
                var tempMatrix = Gizmos.matrix;
                Gizmos.matrix = carLight.transform.localToWorldMatrix;
                Gizmos.DrawFrustum(Vector3.zero, 45f, _carSettings.lightLenght, 0f, 1f);
                Gizmos.matrix = tempMatrix;
            }
        } 

        [ContextMenu("IncreaseDodgeScore")]
        private void IncreaseDodgeScore() {
            _carSettings.dodgeScore++;
        }
    }
}