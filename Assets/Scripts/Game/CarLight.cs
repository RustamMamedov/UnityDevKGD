using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace Game {

    public class CarLight : MonoBehaviour {
        
        [SerializeField] 
        private CarSettings _carSettings; 
 
        [SerializeField] 
        private Color _color = Color.yellow; 
        
        private Light _spotLight;

        private void OnEnable() {
            _spotLight = GetComponent<Light>();
            _spotLight.range = _carSettings.light;
        }

        private void OnDrawGizmos() { 
            Gizmos.color = _color; 
            Gizmos.matrix = transform.localToWorldMatrix; 
            Gizmos.DrawFrustum(Vector3.zero, 45f, 0f, _carSettings.light, 2f); 
 
        } 
    }
}