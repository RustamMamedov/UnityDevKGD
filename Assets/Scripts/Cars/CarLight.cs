using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    public class CarLight : MonoBehaviour {
        [SerializeField]
        private CarsSettings _settings;

        [SerializeField]
        private Light _light;
        private void OnDrawGizmos() {
            Gizmos.matrix = transform.localToWorldMatrix;
            _light.range = _settings.lenghLightCar * 10f;
            Gizmos.DrawFrustum(new Vector3(0f, 0f, -1f), 30f, _settings.lenghLightCar, 1f, 1f);
        }

        private void OnEnable() {
            _light.range = _settings.lenghLightCar * 10f;
        }
    }
}