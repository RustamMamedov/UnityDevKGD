using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class CarLight : MonoBehaviour {

        [SerializeField]
        private Color _gizmosColor = Color.white;

        [SerializeField]
        private CarSettings _carSettings;

        private void OnDrawGizmosSelected() {
            var tempMatrix = Gizmos.matrix;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawFrustum(Vector3.zero, 20f, _carSettings.carLightLenght, 0.5f, 3f);
            Gizmos.matrix = tempMatrix;
        }
    }
}
