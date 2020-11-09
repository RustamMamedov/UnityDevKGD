using Events;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game {

    public class Car : MonoBehaviour {

        #region data
        [SerializeField]
        protected CarSettings _carSettings;

        [SerializeField]
        protected EventListener _updateEventListener;

        [SerializeField]
        private EventListener _carCollisionEventListener;

#if UNITY_EDITOR
        public CarSettings CarSettings => _carSettings;
#endif
        protected float _currentSpeed;

        #endregion data

        #region EventMethods
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
        #endregion EventMethods

        #region Eneble\Disable
        private void OnEnable() {
            SubscribeToEvents();
        }
        private void OnDisable() {
            UnsubscribeToEvents();
        }
        #endregion Eneble\Disable

        #region Move
        protected virtual void Move() {
            if (_currentSpeed < _carSettings.maxSpeed) {
                _currentSpeed += _carSettings.acceleration;
            }
            transform.Translate(transform.forward * _currentSpeed * Time.deltaTime, Space.World);
        }
        #endregion Move

        [ContextMenu("IncreaseDodgeScore")]
        private void IncreaseDodgeScore() {
            _carSettings.dodgeScore++;
        }
    }
}