using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;


namespace Game {

    public class RoadPart : MonoBehaviour {

        [SerializeField]
        private EventDispatcher _roadTriggerEventDispatcher;

        private string _tag = "Player";

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag(_tag)) {
                _roadTriggerEventDispatcher.Dispatch();
            }
        }
    }
}
