using Events;
using UnityEngine;

namespace Game {

    public class Car : MonoBehaviour {

        #region Variables

        [SerializeField]
        protected CarSettings _carSettings;

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private EventListener _carCollisionEventListener;

#if UNITY_EDITOR
        public CarSettings CarSettings => _carSettings;
#endif

        protected float _currentSpeed;

        #endregion

        #region OnEnable / OnDisable

        private void OnEnable() {
            SubscribeToEvents();
        }

        private void OnDisable() {
            UnsubscribeToEvents();
        }

        #endregion

        #region Subscribe / Unsubscribe to Events

        protected virtual void SubscribeToEvents() {
            _updateEventListener.OnEventHappened += UpdateBehaviour;
            _carCollisionEventListener.OnEventHappened += OnCarCollision;
        }

        protected virtual void UnsubscribeToEvents() {
            _updateEventListener.OnEventHappened -= UpdateBehaviour;
            _carCollisionEventListener.OnEventHappened -= OnCarCollision;
        }

        #endregion

        #region Events handlers

        private void UpdateBehaviour() {
            Move();
        }

        private void OnCarCollision() {
            UnsubscribeToEvents();
        }

        #endregion

        #region Move

        protected virtual void Move() {
            if (_currentSpeed < _carSettings.maxSpeed) {
                _currentSpeed += _carSettings.acceleration;
            }
            transform.Translate(transform.forward * _currentSpeed * Time.deltaTime, Space.World);
        }
        #endregion

        [ContextMenu("IncreaseDodgeScore")]
        private void IncreaseDodgeScore() {
            _carSettings.dodgeScore++;
        }
    }
}
