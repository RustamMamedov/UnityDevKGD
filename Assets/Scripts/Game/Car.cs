using System;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Sirenix.OdinInspector;

namespace Game {
    public class Car : MonoBehaviour {
#region 1
        [SerializeField]
        [Required]
        protected CarsSettings _carSetting;

        [SerializeField]
        protected EventListeners _updateEventListeners;

        [SerializeField]
        private EventListeners _carCollisionEventListener;

        [SerializeField]
        private ScriptableIntValue _currentScore;

        [SerializeField]
        private EventListeners _carDodge;

#if UNITY_EDITOR
        public CarsSettings CarsSettings => _carSetting;
#endif
        private bool _dodged = false;

        protected float _currentSpeed;
#endregion 1

#region 3
        private void OnEnable() {
            SubscribeToEvent();
            _dodged = false;
        }
        private void OnDisable() {
            UnsubscribeToEvent();
        }
#endregion 3

#region 2
        protected virtual void SubscribeToEvent() {
            _updateEventListeners.OnEventHappened += UpdateBehaviour;
            _carCollisionEventListener.OnEventHappened += OnCarCollision;
            _carDodge.OnEventHappened += OnCarDodged;
        }
        protected virtual void UnsubscribeToEvent() {
            _updateEventListeners.OnEventHappened -= UpdateBehaviour;
            _carCollisionEventListener.OnEventHappened -= OnCarCollision;
            _carDodge.OnEventHappened -= OnCarDodged;
        }
        protected virtual void OnCarCollision() {
            UnsubscribeToEvent();
        }
        private void UpdateBehaviour() {
            Move();
        }

        private void OnCarDodged() {
            if (!_dodged) {
                _dodged = true;
                _currentScore.value += _carSetting.dodgedScore;
            }
        }
#endregion 2

#region 4
        protected virtual void Move() {
            if(_currentSpeed < _carSetting.maxSpeed) {
                _currentSpeed += _carSetting.acceleration;
            }
            transform.Translate(transform.forward * _currentSpeed * Time.deltaTime, Space.World);
        }
        [ContextMenu("IncreaseDodgeScore")]
        private void IncreaseDodgeScore() {
            _carSetting.dodgedScore++;
        }
#endregion 4
    }
}
