using UnityEngine;
using Events;

namespace Game {
    
    public class EnemyCar : Car {

        [SerializeField]
        private EventDispatcher _enemyCarsTriggerEventDispatcher;

        [SerializeField]
        private EventDispatcher _enemyCarsBackTriggerEventDispatcher;

        [SerializeField]
        private ScriptableIntValue _dodgedScore;

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                _enemyCarsTriggerEventDispatcher.Dispatch();
            }
        }
        
        private void OnTriggerExit(Collider other) {
            if (other.CompareTag("CarBack")) {
                _dodgedScore.value = _carSettings.dodgeScore;
                _enemyCarsBackTriggerEventDispatcher.Dispatch();
            }
        }
    }
}