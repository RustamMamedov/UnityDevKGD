using Events;
using Sirenix.OdinInspector;
using UnityEngine;
using Audio;

namespace Game {

    public class Car : MonoBehaviour {

#region  1
        [SerializeField]
        [Required]
        protected CarSettings _carSettings;

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private EventListener _carCollisionEventListener;

        [SerializeField]
        private AudioSourcePlayer _starCollisionAudio;

        [SerializeField]
        private EventListener _onStarCollisionEventListner;

        public string Name => _carSettings.name;

#if UNITY_EDITOR
        public CarSettings CarSettings => _carSettings;
#endif

        protected float _currentSpeed;

#endregion  1

#region  3
        private void OnEnable() {
            SubscribeToEvents();
        }

        private void OnDisable() {
            UnsubscribeToEvents();
        }

#endregion  3

#region  2
        protected virtual void SubscribeToEvents() {
            _updateEventListener.OnEventHappened += UpdateBehaviour;
            _carCollisionEventListener.OnEventHappened += OnCarCollision;
            _onStarCollisionEventListner.OnEventHappened += OnCarCollision;
        }

        protected virtual void UnsubscribeToEvents() {
            _updateEventListener.OnEventHappened -= UpdateBehaviour;
            _carCollisionEventListener.OnEventHappened -= OnCarCollision;
            _onStarCollisionEventListner.OnEventHappened -= OnCarCollision;

        }

        private void OnStarCollision() {
            _starCollisionAudio.Play();
        }

        private void OnCarCollision() {
            UnsubscribeToEvents();
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

        [ContextMenu("IncreaseDodgeScore")]
        private void IncreaseDodgeScore() {
            _carSettings.dodgeScore++;
        }

#endregion  4
    }
}