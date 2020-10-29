using Events;
using UnityEngine;

namespace Game
{
    public class EnemyCar : Car
    {
        [SerializeField]
        private EventDispatcher _carTriggerEventDispatcher;

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                _carTriggerEventDispatcher.Dispatch();
            }
        }

    }
}
