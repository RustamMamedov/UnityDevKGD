using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using TMPro;
using Events;

namespace Game {

    public class EnemyCar:Car {
        [SerializeField]
        private EventDispatcher eventDispatcher;
        private void OnTriggerEnter(Collider other) {
            Debug.Log("CarCollision");
            



        }
    }
}