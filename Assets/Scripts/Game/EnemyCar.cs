using UnityEngine;
using Events;

namespace Game{
    public class EnemyCar : Car
    {
        [SerializeField]
        private EventDispatcher _enemyCarsTriggerEventDispatch;

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                _enemyCarsTriggerEventDispatch.Dispatch();
            }
        }
    }
}