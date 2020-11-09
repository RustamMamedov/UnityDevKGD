using System.Collections;
using UnityEngine;
using Events;

namespace Game {

    public class EnemyCar : Car {

        [SerializeField]
        private EventDispatcher _carTriggerEventDispatcher;

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                
                Save.Instance.StartSaveProcess();
                _carTriggerEventDispatcher.Dispatch();
            }
        }

    }
}