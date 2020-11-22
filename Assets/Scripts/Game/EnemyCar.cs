using Events;
using UnityEngine;

namespace Game {

    public class EnemyCar : Car {
        
        [SerializeField]
        private EventDispatcher _collisionTriggerED;

        [SerializeField] 
        private EventDispatcher _carDodgedDispatcher;

        [SerializeField] 
        private ScriptableFloatValue _playerPositionZValue;

        [SerializeField] 
        private ScriptableIntValue _dodgedScore;

        [SerializeField] 
        private ScriptableStringValue _carDodgedName;

        [SerializeField] 
        private float _dodgeDistance;

        private bool _isCarDodged = false;

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                _collisionTriggerED.Dispatch();
                Debug.Log("EnemyCar collision");
            }
        }

        protected override void Move() {
            base.Move();

            if (!_isCarDodged && transform.position.z + _dodgeDistance < _playerPositionZValue.value) {
                _isCarDodged = true;
                _dodgedScore.value += _carSettings.dodgeScore;
                _carDodgedName.value = _carSettings.name;
                _carDodgedDispatcher.Dispatch();
            }
        }
    }
}

