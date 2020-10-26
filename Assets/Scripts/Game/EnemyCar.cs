using Events;
using UnityEngine;

namespace Game {
    
    public class EnemyCar : Car
    {
        
        [SerializeField]
        private EventDispatcher _playerCarCollisionDispatcher;
        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag("Player")) {
                _playerCarCollisionDispatcher.Dispatch();
                Debug.Log("EnemyCar collision!");
            }
        }
    }
}

