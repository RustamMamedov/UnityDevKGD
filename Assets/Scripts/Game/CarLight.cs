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
        private ScriptableIntValue _timeLight;

       // private void OnDrawGizmos() {
        //    Gizmos.matrix = transform.localToWorldMatrix;
        //    Gizmos.DrawFrustum(new Vector3(0f, 0f, -1f), 30f, _car.CarSettings.lightDistance, 1f, 1f);
        //    _light.range = _car.CarSettings.lightDistance * 10f;
        //}
        
        private void OnEnable() {
            if (_timeLight.value == 1f) {
                _light.gameObject.SetActive(true);
            }
            else {
                _light.gameObject.SetActive(false);
            }
            _light.range = _car.CarSettings.lightDistance * 10f;
        }
    }
}