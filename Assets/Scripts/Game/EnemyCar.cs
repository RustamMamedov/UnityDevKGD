using UnityEngine;
using Events;

namespace Game{
    public class EnemyCar : Car
    {
        [SerializeField]
        private EventDispatcher _enemyCarsTriggerEventDispatch;

        [SerializeField]
        private EventDispatcher _enemyCarsBackTriggerEventDispatcher;

        [SerializeField]
        private ScriptableIntValue _dodgedScore;

        private void OnTriggerEnter(Collider other){
            if (other.CompareTag("Player")) {
                _enemyCarsTriggerEventDispatch.Dispatch();
            }
        }
        
        private void OnTriggerExit(Collider other) {
            if (other.CompareTag("CarBack")) {
                _dodgedScore.value = _carSettings.dodgeScore;
                _enemyCarsBackTriggerEventDispatcher.Dispatch();
                Debug.Log("erer");
            }
        }
    }
}