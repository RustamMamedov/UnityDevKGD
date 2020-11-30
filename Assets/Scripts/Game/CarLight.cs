using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class CarLight : MonoBehaviour {

        [SerializeField]
        private CarSettings _car;

        [SerializeField]
        private Light _light;

        [SerializeField]
        private ScriptableIntValue _illumination;

        //private void OnDrawGizmos() {
         //   Gizmos.matrix = transform.localToWorldMatrix;
        //    Gizmos.DrawFrustum(new Vector3(0f, 0f, -1f), 45f, _car.lightlenght, 1f, 1f);
        //    _light.range = _car.lightlenght*20f;
        //}

        private void OnEnable() {
            if (_illumination.value == 1) {
                _light.gameObject.SetActive(true);
            } else {
                _light.gameObject.SetActive(false);
            }
            _light.range = _car.lightlenght * 20f;
        }
    }
}

