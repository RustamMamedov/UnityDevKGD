using System;
using System.Collections.Generic;
using UnityEngine;
using Events;
using UI;

namespace Game {
    public class Car : MonoBehaviour {

        #region region1

        [SerializeField]
        protected CarSettings _carSettings;

        [SerializeField]
        private EventListeners _updateEventListeners;

        [SerializeField]
        private EventListeners _carCollisionEventListeners;

        protected float _currentSpeed=0f;

        #endregion region1

        #region region2
        protected virtual void SubscribeToEvents() {
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
            UIManager.Instance.LoadLeaderBoard();
            //Debug.Log("CarCollision");
        }
        #endregion region2

        #region region3
        private void OnEnable() { 
            SubscribeToEvents();
        }

        private void OnDisable() {
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
