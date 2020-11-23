using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class CarLight : MonoBehaviour {

        [SerializeField]
        private CarSettings _carSettings;

        [SerializeField]
        private Light _light;

        private float _offset = 10f;

        private void Awake() {
            _light.range = _carSettings.lightDistance * _offset;
        }
    }
}
