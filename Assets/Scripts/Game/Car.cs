using Events;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game {

    public class Car : MonoBehaviour {

#region  1
        [SerializeField]
        [Required]
        public CarSettings carSettings;

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private EventListener _carCollisionEventListener;



        public string Name => carSettings.name;

#if UNITY_EDITOR
        public CarSettings CarSettings => carSettings;
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
#endregion  2

#region  4
        protected virtual void Move() {
            if (_currentSpeed < carSettings.maxSpeed) {
                _currentSpeed += carSettings.acceleration;
            }
            transform.Translate(transform.forward * _currentSpeed * Time.deltaTime, Space.World);
        }

        [ContextMenu("IncreaseDodgeScore")]
        private void IncreaseDodgeScore() {
            carSettings.dodgeScore++;
        }

#endregion  4
    }
}