using System;
using Events;
using UnityEngine;

namespace Game {
    
    public class RoadPart : MonoBehaviour {

        [SerializeField] 
        private EventDispatcher _roadTriggerEventDispatcher;

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                Debug.Log("Road collision");
                _roadTriggerEventDispatcher.Dispatch();
            }
        }
    }
}

