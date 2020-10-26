﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;


namespace Game {


    public class Car : MonoBehaviour {

        #region  1
        [SerializeField]
        protected CarSettings _carSettings;

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private EventListener _carCollisionEventListener;

        protected float _currentSpeed;

        #endregion  1

        #region  3
        protected virtual void OnEnable() {
            SubscribeToEvents();
        }

        protected virtual void OnDisable() {
            UnsubscribeToEvents();
        }

        #endregion  3

        #region  2
        private void SubscribeToEvents() {
            _updateEventListener.OnEventHappend += UpdateBehaviour;
            _carCollisionEventListener.OnEventHappend += OnCarCollision;
        }

        protected virtual void UnsubscribeToEvents() {
            _updateEventListener.OnEventHappend -= UpdateBehaviour;
            _carCollisionEventListener.OnEventHappend -= OnCarCollision;
        }

        private void OnCarCollision() {
            //UnsubscribeToEvents();
        }

        private void UpdateBehaviour() {
            Move();
        }
        #endregion  2

        #region  4
        protected virtual void Move() {
            if (_currentSpeed < _carSettings.maxSpeed) {
                _currentSpeed += _carSettings.acceleration;
            }
            transform.Translate(transform.forward * _currentSpeed * Time.deltaTime, Space.World);
        }

        #endregion  4
    }
}
