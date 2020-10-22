using System;
using System.Collections.Generic;
using UnityEngine;
using Events;

namespace Game {
    public class Car : MonoBehaviour {

        #region region1

        [SerializeField]
        private CarSettings _carSettings;

        [SerializeField]
        private EventListeners _updateEventListeners;

        [SerializeField]
        private EventListeners _carCollisionEventListeners;

        protected float _currentSpeed=0f;

        #endregion region1

        #region region2
        private void SubscribeToEvents() {
            _updateEventListeners.OnEventHappened += UpdateBehavour;
            _carCollisionEventListeners.OnEventHappened += OnCarCollision;
        }

        protected virtual void UnsubscribeToEvents() {
            _updateEventListeners.OnEventHappened -= UpdateBehavour;
            _carCollisionEventListeners.OnEventHappened -= OnCarCollision;
        }

        private void UpdateBehavour() {
            Move();
        }

        private void OnCarCollision() {
            UnsubscribeToEvents();
        }
        #endregion region2

        #region region3
        protected virtual void OnEnable() { 
            SubscribeToEvents();
        }

        protected virtual void OnDisable() {
            UnsubscribeToEvents();
        }
        #endregion region3

        #region region4

        protected virtual void Move() {
           if (_currentSpeed < _carSettings.maxSpeed) {
                _currentSpeed += _carSettings.acceleration;
            }
            transform.Translate(transform.forward * _currentSpeed * Time.deltaTime, Space.World);
        }

        #endregion region4


    }
}
