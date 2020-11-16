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
            Gizmos.DrawFrustum(transform.position, 30f,7f, _car.CarSettings.CarLightLength, 1f);
        }
    }
}