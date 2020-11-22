using Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;

namespace Game {
    public class EnemyCar : Car {

        [SerializeField]
        private EventDispatcher _carCollisionDispatcher;

        [SerializeField]
        private BoxCollider _boxCollider;

        [SerializeField]
        private ScriptableFloatValue _playerSize;

        [SerializeField]
        private ScriptableIntValue _score;

        [SerializeField]
        private EventDispatcher _carDodge;

        [SerializeField]
        private ScriptableFloatValue _carPositionZ;

        [SerializeField]
        private AudioSourcePlayer _audioSourcePlayer;

        [SerializeField]
        private ScriptableIntValue _counter;

        private void IsDodge() {
            if (_carPositionZ.value - transform.position.z > _playerSize.value + _boxCollider.size.z / 2) {
                _carDodge.Dispatch();
                _score.value += _carSettings.dodgeScore;
                _updateEventListener.OnEventHappened -= IsDodge;
                _audioSourcePlayer.Play();
                _counter.value ++;
            }
        }

        protected override void SubscribeToEvents() {
            base.SubscribeToEvents();
            _updateEventListener.OnEventHappened += IsDodge;
        }

        protected override void UnsubscribeToEvents() {
            base.UnsubscribeToEvents();
            _updateEventListener.OnEventHappened -= IsDodge;
        }

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                _carCollisionDispatcher.Dispatch();
            }
        }
    }
}