using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class CarLight : MonoBehaviour {

        [SerializeField]
        private PlayerCar _playerCar;

        [SerializeField]
        private Light _carLight;

        private void Awake() {
            _carLight.range = 15 * _playerCar._carSettings.carLightDistance;
        }
    }
}