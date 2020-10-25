using System;
using System.Collections.Generic;
using UnityEngine;
using Events;


namespace Game {
    public class Car : MonoBehaviour {
        #region 1
        [SerializeField]
        protected CarSettings _carSetting;

        [SerializeField]
        private EventListener _updateEventListeners;

        [SerializeField]
        private EventListener _carCollisionEventListener;

        protected float _currentSpeed;
        #endregion 1

        #region 3
        protected virtual void OnEnable() {
            SubscribeToEvent();
        }
        protected virtual void OnDisable() {
            UnsubscribeToEvent();
        }
        #endregion 3

        #region 2
        private void SubscribeToEvent() {
            _updateEventListeners.OnEventHappened += UpdateBehavior;
            _carCollisionEventListener.OnEventHappened += OnCarCollision;
        }
        protected virtual void UnsubscribeToEvent() {
            _updateEventListeners.OnEventHappened -= UpdateBehavior;
            _carCollisionEventListener.OnEventHappened -= OnCarCollision;
        }
        private void OnCarCollision() {
            UnsubscribeToEvent();
        }
        private void UpdateBehavior() {
            Move();
        }
        #endregion 2

        #region 4
        protected virtual void Move() {
            if (_currentSpeed < _carSetting.maxSpeed) {
                _currentSpeed += _carSetting.acceleration;
            }
            transform.Translate(transform.forward * _currentSpeed * Time.deltaTime, Space.World);
        }
        #endregion 4
    }
}