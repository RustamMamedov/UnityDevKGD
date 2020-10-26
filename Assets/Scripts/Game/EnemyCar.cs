using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    public class EnemyCar : Car {
        [SerializeField]
        private GameObject _familyCarPrefab;
        [SerializeField]
        private GameObject _suvPrefab;
        [SerializeField]
        private GameObject _truckPrefab;

    }
}