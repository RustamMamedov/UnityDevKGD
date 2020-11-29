using System;
using UnityEngine;

namespace Game {
    
    [RequireComponent(typeof(Light))]
    public class CarLight : MonoBehaviour {

        [SerializeField] 
        private Color _gizmoColor = Color.yellow;

        [SerializeField] 
        private CarSettings _carSettings;

        private Light _light;

        private void Awake() {
            _light = GetComponent<Light>();
            _light.range = _carSettings.lightDistance;
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = _gizmoColor;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawFrustum(Vector3.zero, 45f, 0f, _carSettings.lightDistance, 1f);
        }
    }
}

