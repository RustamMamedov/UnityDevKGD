using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game { 

    public class CarLight : MonoBehaviour {

        [SerializeField]
        private Car _car;

        [SerializeField]
        private Light _light;

        [SerializeField]
        private ScriptableIntValue _timeMode;

        private void Awake() {
            if (_timeMode.value == 0) {
                gameObject.SetActive(false);
            }
            else
                gameObject.SetActive(true);
        }

        private void OnDrawGizmos() {
            //Gizmos.color = _colorLight;
            //Gizmos.matrix = transform.localToWorldMatrix;
            //Gizmos.DrawFrustum(new Vector3(0f, 0f, -1f), 30f, _car.CarSettings.carLightLength, 1f, 1f);
            //_light.range = _car.CarSettings.carLightLength * 10f;
        }
    }
}