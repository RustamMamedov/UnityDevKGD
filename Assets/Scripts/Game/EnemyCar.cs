using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using UI;

namespace Game {
    public class EnemyCar : Car {

        [SerializeField]
        private EventDispatcher _carCollisionEventDispatcher;

        [SerializeField]
        private ScriptableFloatValue _playerPositionZ;

        [SerializeField]
        private ScriptableIntValue _currentScore;

        private bool _isAdded = false;

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                _carCollisionEventDispatcher.Dispatch();
                Debug.Log("Crashed with: " + transform.name);
            }
        }

        protected override void UpdateBehaviour() {
            base.UpdateBehaviour();
            AddDodgeScore();
        }

        private void AddDodgeScore() {
            if(transform.position.z < _playerPositionZ.value && !_isAdded) {
                _currentScore.value += _carSettings.dodgeScore;
                _isAdded = true;
            }
        }
    }

}
