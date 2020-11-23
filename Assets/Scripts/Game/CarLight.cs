using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace Game {

    public class CarLight : MonoBehaviour {

        [SerializeField]
        private Color _gizmosColor = Color.white;

        [SerializeField]
        private CarSettings _carSettings;

        private Light _spotLight;

        private void OnEnable() {
            _spotLight = GetComponent<Light>();
            _spotLight.range = _carSettings.lightLength;
        }

        private void OnDrawGizmos() {
            Gizmos.color = _gizmosColor;
            Gizmos.DrawFrustum(transform.position, 90f, _carSettings.lightLength, 0f, .5f);
        }
    }
}
