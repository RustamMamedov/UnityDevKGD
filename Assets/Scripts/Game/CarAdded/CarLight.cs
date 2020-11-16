using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class CarLight : MonoBehaviour {

        [SerializeField]
        private CarSettings _settings;

        private void OnDrawGizmos() {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawFrustum(transform.forward, 30f, _settings.lenghLightCar + 1, 1f, 1f);
        }

    }
}
