using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class CarLight : MonoBehaviour {

        [SerializeField]
        private CarSettings _settings;

        [SerializeField]
        private Light _light;

        [SerializeField]
        private float _constantLenghCar;

        private void OnEnable() {
            _light.range = _settings.lenghLightCar * _constantLenghCar;
        }

        private void OnDrawGizmos() {
            _light.range = _settings.lenghLightCar * _constantLenghCar;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawFrustum(new Vector3(0f,0f,-1f), 30f, _settings.lenghLightCar* _constantLenghCar, 1f, 1f);
        }



    }
}
