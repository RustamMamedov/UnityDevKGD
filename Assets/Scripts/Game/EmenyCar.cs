using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Audio;

namespace Game {

    public class EmenyCar : Car {
        [SerializeField]
        private EventDispatcher _carCollisionDispatcher;

        [SerializeField]
        private ScriptableFloatValue _sizePlayerCar;
        
        [SerializeField]
        private BoxCollider _sizeBoxCollider;

        [SerializeField]
        private ScriptableFloatValue _playerCarPositionZ;

        [SerializeField]
        private ScriptableIntValue _score;

        [SerializeField]
        private EventDispatcher _carDodge;

        [SerializeField]
        private AudioSoursePlayer _dodgePlayer;

        [SerializeField]
        private ScriptableIntValue _typeScore;


        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                _carCollisionDispatcher.Dispatch();
                _score.value = 0;
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
            if (_playerCarPositionZ.value-transform.position.z>_sizePlayerCar.value+_sizeBoxCollider.size.z*.5f) {
                    _updateEventListener.OnEventHappened -= IsDodged;
                    _score.value += _carSettings.dodgeScore;
                    _typeScore.value++;
                    _dodgePlayer.Play();
                    _carDodge.Dispatch();
                }
        }
    }
}