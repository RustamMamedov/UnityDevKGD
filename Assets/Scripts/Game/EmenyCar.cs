using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

namespace Game {
    public class EmenyCar : Car {
        [SerializeField]
        private EventDispatcher _carCollisionDispatcher;

        [SerializeField]
        private float _sizePlayerCar;//TODO: переделать через scriptableFloatValue чтобы адаптивна была
        
        [SerializeField]
        private BoxCollider _sizeBoxCollider;

        [SerializeField]
        private ScriptableFloatValue _playerCarPositionZ;

        [SerializeField]
        private ScriptableIntValue _score;

        [SerializeField]
        private EventDispatcher _carDodge;


        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                _carCollisionDispatcher.Dispatch();
            }
        }

        protected override void SubscribeToEvents() {
            base.SubscribeToEvents();
            _updateEventListener.OnEventHappened += IsDodged;
        }

        protected override void UnsubscribeToEvents() {
            base.UnsubscribeToEvents();
            _updateEventListener.OnEventHappened -= IsDodged;
        }

        private void IsDodged() {
            if (_playerCarPositionZ.value-transform.position.z>_sizePlayerCar+_sizeBoxCollider.size.z*.5f) {
                    _updateEventListener.OnEventHappened -= IsDodged;
                    _score.value += _carSettings.dodgeScore;
                    _carDodge.Dispatch();
                }
        }
    }
}