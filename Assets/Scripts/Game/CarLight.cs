using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class CarLight : MonoBehaviour {

        [SerializeField]
        private Color _gizmosColor = Color.white;

        [SerializeField]
        private CarSettings _carSettings;

        [SerializeField]
        private Light _light;

        private void Awake() {
            if (_light == null) {
                return;
            }

            _light.range = _carSettings.CarLight * 10f;
        }

        private void OnDrawGizmosSelected() {

            Gizmos.color = _gizmosColor;
            var tempMatrix = Gizmos.matrix;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawFrustum(Vector3.zero, 45f, 0f, _carSettings.CarLight, 1f);
            Gizmos.matrix = tempMatrix;
        }
    }
}