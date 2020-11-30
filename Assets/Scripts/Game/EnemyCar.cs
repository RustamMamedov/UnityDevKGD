using UnityEngine;
using Events;

namespace Game {

    public class EnemyCar : Car {

        [SerializeField]
        private EventDispatcher _carCollisionEventDispatcher;

        [SerializeField]
        private EventDispatcher _dodgeDispatcher;

        [SerializeField]
        private ScriptableIntValue _dodgeScore;

        [SerializeField]
        private ScriptableStringValue _carName;

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                _carCollisionEventDispatcher.Dispatch();
            }
            if (other.CompareTag("DodgeObject")) {
                _carName.value = _carSettings.name;
                _dodgeScore.value = _carSettings.dodgeScore;
                _dodgeDispatcher.Dispatch();
            }
        }
    }
}
