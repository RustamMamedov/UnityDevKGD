﻿using Events;
using UnityEngine;
using Game;
using UnityEngine.Scripting.APIUpdating;

namespace Game {

    public class Car : MonoBehaviour {
        #region 1
        [SerializeField]
        protected CarSettings _carSettings;

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private EventListener _carCollisionEvenetListener;

        protected float _currentSpeed;
        #endregion 1

        #region 2
        private void SubcribeToEvents() {
            _updateEventListener.OnEventHappened += UpdateBehaviour;
            _carCollisionEvenetListener.OnEventHappened += OnCarCollision;
        }

        protected virtual void UnsubscribeToEvents() {
            _updateEventListener.OnEventHappened -= UpdateBehaviour;
            _carCollisionEvenetListener.OnEventHappened -= OnCarCollision;
        }

        private void OnCarCollision() {
           UnsubscribeToEvents();
        }

        private void UpdateBehaviour() {
            Move();
        }
        #endregion 2

        #region 3
        private void OnEnable() {
            SubcribeToEvents();
        }
        private void OnDisable() {
            UnsubscribeToEvents();
        }
        #endregion 3

        #region 4
        protected virtual void Move() {
            if (_currentSpeed < _carSettings.maxSpeed) {
                _currentSpeed += _carSettings.maxSpeed;
            }
            transform.Translate(transform.forward * _currentSpeed * Time.deltaTime, Space.World);
        }
        #endregion 4
    }
}