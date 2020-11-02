using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using UI;

namespace Game {

    public class EnemyCar : Car {

        [SerializeField]
        private EventDispatcher _carTriggerEventDispatcher;

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                UIManager.Instance.ShowLeaderboardsScreen();
                Debug.Log("CarCollision");
                _carTriggerEventDispatcher.Dispatch();
            }
        }
    }
}
