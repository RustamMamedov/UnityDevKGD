using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

namespace Game {
    public class EnemyCar : Car{
        [SerializeField]
        private EventDispatcher _enemyCarTriggerEventDispatcher;

        [SerializeField]
        private EventDispatcher _carDodge;

        [SerializeField]
        private ScriptableIntValue _scoreValue;

        [SerializeField]
        private BoxCollider _ownBoxCollider;

        [SerializeField]
        private ScriptableFloatValue _carPlayerSize;

        [SerializeField]
        private ScriptableFloatValue _playerPosition;

        [SerializeField]
        private ScriptableIntValue _counter;


        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                //Debug.Log("CarCollision");
                //_scoreValue.value = 0;
                _enemyCarTriggerEventDispatcher.Dispatch();
            }
        }

        private void IsDodged() {
            if (_playerPosition.value - transform.position.z > _ownBoxCollider.size.z/2+_carPlayerSize.value) {
                _scoreValue.value += _carSetting.dodgeScore;
                _carDodge.Dispatch();
                _updateEventListeners.OnEventHappened -= IsDodged;
                _counter.value++;
            }
        }

        protected override void SubscribeToEvent() {  //переопределяем
            base.SubscribeToEvent();
            _updateEventListeners.OnEventHappened += IsDodged; 
        }
        protected override void UnsubscribeToEvent() {
            base.UnsubscribeToEvent();
            _updateEventListeners.OnEventHappened -= IsDodged;
        }
    }
}
