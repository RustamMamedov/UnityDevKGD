using Events;
using UnityEngine;

namespace Game {

    public class EnemyCar : Car 
    {
        [SerializeField]
        private EventDispatcher _collisionTriggerED;

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                _collisionTriggerED.Dispatch();
                Debug.Log("EnemyCar collision");
            }
        }
    }
}

