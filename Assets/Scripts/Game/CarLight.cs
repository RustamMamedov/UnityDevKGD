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

            Gizmos.color = _gizmosColor;
            var tempMatrix = Gizmos.matrix;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawFrustum(Vector3.zero, 45f, 15f, 3f, 1f);
            Gizmos.matrix = tempMatrix;
        }
    }
}