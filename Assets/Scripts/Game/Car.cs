using Events;
using UnityEngine;
using Sirenix.OdinInspector;


namespace Game {

    public class Car : MonoBehaviour {

        #region 1
        [SerializeField]
        [Required]
        protected CarSettings _carSettings;

        [SerializeField]
        protected EventListener _updateEventListener;

        [SerializeField]
        private EventListener _carCollisionEventListener;

        public CarSettings CarSettings => _carSettings;

        protected float _currentSpeed;

        #endregion 1

        #region 3
        private void OnEnable() {
            SubscribeToEvents();
        }

        private void OnDisable() {
            UnsubscribeToEvents();
        }

        #endregion 3

        #region 2
        protected virtual void SubscribeToEvents() {
            _updateEventListener.OnEventHappened += UpdateBehaviour;
            _carCollisionEventListener.OnEventHappened += OnCarCollision;
        }

        protected virtual void UnsubscribeToEvents() {
            _updateEventListener.OnEventHappened -= UpdateBehaviour;
            _carCollisionEventListener.OnEventHappened -= OnCarCollision;
        }

        protected virtual void OnCarCollision() {
            UnsubscribeToEvents();
        }

        private void UpdateBehaviour() {
            Move();
        }

        #endregion 2

        #region 4

        protected virtual void Move() {
            if (_currentSpeed < _carSettings.maxSpeed) {
                _currentSpeed += _carSettings.acceleration;
            }
            transform.Translate(transform.forward * _currentSpeed * Time.deltaTime, Space.World);
        }

        [ContextMenu("IncreaseDodgeScore")]
        private void IncreaseDodgeScore() {
            _carSettings.dodgeScore++;
        }

        #endregion 4
    }
}