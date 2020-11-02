using System.Collections;
using System.Collections.Generic;
using Events;
using UI;
using UnityEngine;

namespace Game {

	public class EnemyCar : Car {
        
        [SerializeField] 
        private EventDispatcher _carTriggerEventDispatcher;

        [SerializeField] 
        private ScriptableIntValue _currentScoreAsset;
        
        [SerializeField]
        private ScriptableFloatValue _playerPositionZ;

        [SerializeField] 
        private ScriptableFloatValue _playerPositionX;

        [SerializeField] 
        private int _distanceToDodge = - 40;
        private bool _enemyIsDodged = false;

        private void CheckIfDodged() {
	        Debug.Log(Mathf.Ceil(transform.position.x) + " " + Mathf.Ceil(_playerPositionX.value));
            if (Mathf.Ceil(transform.position.x) == Mathf.Ceil(_playerPositionX.value)) {
                if (_playerPositionZ.value - transform.position.z > _distanceToDodge) _enemyIsDodged = true;
            }
        }
        private void AddScore() {
            if(_enemyIsDodged) _currentScoreAsset.value += _carSettings.dodgeScore; 
        }
        
        protected override void Move() {
            base.Move();
            CheckIfDodged();
        }

        protected override void OnDisable() {
            base.OnDisable();
            AddScore();
        }
        
        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                _carTriggerEventDispatcher.Dispatch();
                Debug.Log("Collision with " + transform.name);
                UIManager.Instance.ShowLeaderboardScreen();
            }
        }
    }
}