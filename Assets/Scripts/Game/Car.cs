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

        [SerializeField]
        private EventListeners _carDodgedEventListeners;

        [SerializeField]
        private ScriptableIntValue _currentScore;

        protected float _currentSpeed=0f;
        private bool _dodged = false;

        #endregion region1

        #region region2
        protected virtual void SubscribeToEvents() {
            _updateEventListeners.OnEventHappened += UpdateBehavour;
            _carCollisionEventListeners.OnEventHappened += OnCarCollision;
            _carDodgedEventListeners.OnEventHappened += OnCarDodged;
        }

        protected virtual void UnsubscribeToEvents() {
            _updateEventListeners.OnEventHappened -= UpdateBehavour;
            _carCollisionEventListeners.OnEventHappened -= OnCarCollision;
            _carDodgedEventListeners.OnEventHappened -= OnCarDodged;
        }

        private void UpdateBehavour() {
            Move();
        }

        //private void OnCarCollision() {
        protected virtual void OnCarCollision() { 
            UnsubscribeToEvents();
            //Save.Instance.SaveFromCollision();
            if (UIManager.Instance != null) {
                UIManager.Instance.LoadLeaderBoard();
            }
            //Debug.Log("CarCollision");
        }

        private void OnCarDodged() {
            if (!_dodged) {
                _dodged = true;
                _currentScore.Value += _carSettings.dodgedScore;
            }

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
