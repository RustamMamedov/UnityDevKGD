using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;

namespace Game {

    public class Stars : MonoBehaviour {

        [SerializeField] 
        private int _distanceToPlayerToDestroy;
        
        [SerializeField]
        private ScriptableFloatValue _playerPositionZ;

        [SerializeField] 
        private EventDispatcher _starGotCollectedEventDispatcher;

        [SerializeField] 
        private EventListener _updateEventListener;

        private void OnEnable() {
            SubscribeToEvents();
        }

        private void OnDisable() {
            UnsubscribeFromEvents();
        }

        private void SubscribeToEvents() {
            _updateEventListener.OnEventHappened += UpdateBehaviour;
        }

        private void UnsubscribeFromEvents() {
            _updateEventListener.OnEventHappened -= UpdateBehaviour;
        }

        private void UpdateBehaviour() {
            CheckIfBehindPlayer();
        }
        
        private void CheckIfBehindPlayer() {
            if (_playerPositionZ.value - gameObject.transform.position.z > _distanceToPlayerToDestroy) {
                gameObject.SetActive(false);
            }
        }
        
        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                gameObject.SetActive(false);
                _starGotCollectedEventDispatcher.Dispatch();
            }
        }
    }
}