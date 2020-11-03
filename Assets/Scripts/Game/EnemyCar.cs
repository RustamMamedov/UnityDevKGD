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
        private ScriptableBoolValue _crazyModeEnabled;

        private int _distanceToDodge = 40;
        private bool _enemyIsDodged = false;

        private void CheckIfDodged() {
            if (_crazyModeEnabled.value) {
                if (Mathf.Ceil(transform.position.x) == Mathf.Ceil(_playerPositionX.value)) {
                    if (transform.position.z - _playerPositionZ.value < _distanceToDodge) _enemyIsDodged = true;
                }
            }
            else {
                if (transform.position.z - _playerPositionZ.value < _distanceToDodge) {
                    if (Mathf.Abs(Mathf.Ceil(transform.position.x - _playerPositionX.value)) == 4 || Mathf.Abs(Mathf.Ceil(_playerPositionX.value - transform.position.x)) == 4 )
                        _enemyIsDodged = true;
                }
            }
        }
        private void AddScore() {
            if(_enemyIsDodged) _currentScoreAsset.value += _carSettings.dodgeScore;
            _enemyIsDodged = false;
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
                UIManager.Instance.ShowLeaderboardScreen();
            }
        }
    }
}
