using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    public class CarLightGizmo : MonoBehaviour {

        [SerializeField] private CarSettings _carSettings;

        [SerializeField] private Color _color = Color.white;

        private void OnDrawGizmos() {
            Gizmos.color = _color;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawFrustum(Vector3.zero, 45f, 0f, _carSettings.lightDistance, 2f);
        }
    }
}