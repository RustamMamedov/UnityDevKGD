using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;


namespace Game {

    public class RoadPart : MonoBehaviour {

        [SerializeField]
        private EventDispatcher _roadTriggerEventDispatcher;

        private string tag = "Player";

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag(tag)) {
                _roadTriggerEventDispatcher.Dispatch();
            }
        }
    }
}
