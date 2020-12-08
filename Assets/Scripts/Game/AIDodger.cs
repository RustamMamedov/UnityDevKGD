using System.Collections;
using Events;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    public class AIDodger : MonoBehaviour {
        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private AISettings _distanseToCarSpawner; 

        [SerializeField]
        private ScriptableFloatValue _playerPositionZ;

        private void OnEnable() {
            _updateEventListener.OnEventHappened += UpdateBehaviour;
        }

        private void UpdateBehaviour() {
            
        }
    }
}