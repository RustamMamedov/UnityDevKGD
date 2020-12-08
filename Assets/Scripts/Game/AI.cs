using System;
using Events;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game {
    public class AI : MonoBehaviour {

        [SerializeField] 
        private PlayerCar _playerCar;

        [SerializeField] 
        private EventListener _enemyCarClose;

        [SerializeField] 
        private ScriptableIntValue _lineToDodge;

        
        #region LifeCycle
        
        private void Awake() {
            _enemyCarClose.OnEventHappened += DodgeDecision;
        }

        private void OnDestroy() {
            _enemyCarClose.OnEventHappened -= DodgeDecision;
        }
        
        #endregion

        private void DodgeDecision() {
            if (Random.Range(1, 3) == 1) {
                _playerCar.Dodge(_lineToDodge.value);
            }
        }
    }
}

