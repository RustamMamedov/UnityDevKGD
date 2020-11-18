using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    public class CarLight : MonoBehaviour {

        [SerializeField]
        private Color _colorLight;
        [SerializeField]
        private Car _car;

        private void OnDrawGizmosSelected() {
            Gizmos.color = _colorLight;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawFrustum(new Vector3(0f, 0f, -1f), 30f, _car.CarSettings.carLightLength, 1f, 1f);
        }
    }
}