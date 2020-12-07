using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

namespace Game {
    public class Star : MonoBehaviour {
        [SerializeField]
        private EventDispatcher _onStarCollisionEventDispatcher;

        [SerializeField]
        private EventListener _updateEventListner;

        [SerializeField]
        private ScriptableFloatValue _playerPositionZ;

        [SerializeField]
        private float _distanceToDestroy;

        private void OnEnable() {
            _updateEventListner.OnEventHappened += 
        }

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                _onStarCollisionEventDispatcher.Dispatch();
                Destroy(gameObject);
            }
        }

        private void UpdateBehaviour(){
           if(gameObject.transform.position.z+_distanceToDestroy<_playerPositionZ.value) {
                Destroy(gameObject);
            }
        }

    }
}

