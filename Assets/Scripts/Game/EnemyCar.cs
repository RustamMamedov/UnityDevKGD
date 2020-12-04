using Events;
using System.Drawing;
using UnityEngine;

namespace Game {
    public class EnemyCar : Car {

        [SerializeField]
        private EventDispatcher _carDodgedED;

        [SerializeField]
        private ScriptableFloatValue _playerPositionZValue;

        [SerializeField]
        private ScriptableIntValue _currentScoreValue;

        [SerializeField]
        private ScriptableIntValue _currentIdDodgeCar;

        [SerializeField]
        private BoxCollider _enemyCarBC;

        [SerializeField]
        private EventDispatcher _playerCarCollisionDispatcher;

        private bool _isCarDodged = false;

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                _playerCarCollisionDispatcher.Dispatch();
            }
        }

        protected override void UpdateBehaviour() {
                base.UpdateBehaviour();
                CarDodgedHandler();
        }

        public void SetActive() {
            _isCarDodged = false;
        }

        private void CarDodgedHandler() {
            if (!_isCarDodged) {
                if (transform.position.z < _playerPositionZValue.value - _enemyCarBC.size.z + 1) {
                    _currentScoreValue.value += _carSettings.dodgeScore;
                    _currentIdDodgeCar.value = _carSettings.id;
                    _carDodgedED.Dispatch();

                    _isCarDodged = true;
                }
            }
        }
    }
}