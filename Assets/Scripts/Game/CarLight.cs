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
        private Light _carLight;

        private void Awake() {
            _carLight.range = _carSettings.carLightLength;
        }
        private void OnDrawGizmosSelected() {
            var tempMatrix = Gizmos.matrix;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawFrustum(new Vector3(0f, 0f, -1f), 45f, _carSettings.carLightLength, 1f, 1f);
            Gizmos.matrix = tempMatrix;
        }
    }

}