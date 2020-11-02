using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using UI;


namespace Game {

    public class EnemyCar : Car {

        [SerializeField]
        private EventDispatcher _carTriggerEventDispatcher;

        [SerializeField]
        private ScriptableIntValue _currentScore;

        [SerializeField]
        private ScriptableFloatValue _playerPositionZ;

        [SerializeField]
        private BoxCollider _boxColliderEnemy;
        private bool _dodged = false;


        protected override void UpdateBehaviour() {
            if (_playerPositionZ.value >= gameObject.transform.position.z - _boxColliderEnemy.size.z && !_dodged)   {
                _currentScore.value += _carSettings.dodgeScore;
                _dodged = true;
            }
            base.UpdateBehaviour();
        }

        private void OnTriggerEnter(Collider other) {
        
            if (other.CompareTag("Player")) {
                _carTriggerEventDispatcher.Dispatch();
                UIManager.Instance.ShowLeaderboardScreen();

            }
        }
    }
}