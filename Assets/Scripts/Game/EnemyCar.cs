using UnityEngine;
using Events;

namespace Game {
    public class EnemyCar : Car {

        [SerializeField]
        private EventDispatcher _carCollisionEventDispatcher;

        [SerializeField]
        private EventDispatcher _carDodgedEventDispatcher;

        [SerializeField]
        private ScriptableIntValue _dodgedScore;

        [SerializeField]
        private ScriptableIntValue _typePassedCar;

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                _carCollisionEventDispatcher.Dispatch();
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.CompareTag("PlayerDodged")) {
                _dodgedScore.value = _carSettings.dodgeScore;
                _typePassedCar.value = (int)_carSettings.enemyType;
                _carDodgedEventDispatcher.Dispatch();
            }
        }
    }
}