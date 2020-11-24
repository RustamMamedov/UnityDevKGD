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
        protected EventListener _updateEventListeners;

        [SerializeField]
        private EventListener _carCollisionEventListener;

#if UNITY_EDITOR
        public CarSettings CarSettings => _carSetting;
#endif

        protected float _currentSpeed;
#endregion 1

#region 3
        private void OnEnable() {
            SubscribeToEvent();
        }
        private void OnDisable() {
            UnsubscribeToEvent();
        }
#endregion 3

#region 2
        protected virtual void SubscribeToEvent() {
            _updateEventListeners.OnEventHappened += UpdateBehavior;
            _carCollisionEventListener.OnEventHappened += OnCarCollision;
        }
        protected virtual void UnsubscribeToEvent() {
            _updateEventListeners.OnEventHappened -= UpdateBehavior;
            _carCollisionEventListener.OnEventHappened -= OnCarCollision;
        }
        protected virtual void OnCarCollision() {
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

        [ContextMenu("IncreaseDodgeScore")]
        private void IncreaseDodgeScore() {
            _carSetting.dodgeScore++;
        }


#endregion 4
    }
}