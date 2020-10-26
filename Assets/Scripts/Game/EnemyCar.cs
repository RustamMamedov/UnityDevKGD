using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using Events;

namespace Game {
    public class EnemyCar : Game.Car {

        [SerializeField]
        private EventDispatcher _roadCollisionEventDispatcher;

        private void OnTriggerEnter(Collider other) {
            Debug.Log("CarCollision");
        }
    }
}