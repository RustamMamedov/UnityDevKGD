using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class CarLight : MonoBehaviour {
        
        [SerializeField] 
        private CarSettings _carSettings; 
 
        [SerializeField] 
        private Color _color = Color.yellow; 
        
        private void OnDrawGizmos() { 
            Gizmos.color = _color; 
            Gizmos.matrix = transform.localToWorldMatrix; 
            Gizmos.DrawFrustum(Vector3.zero, 45f, 0f, _carSettings.lightLength, 2f); 
 
        } 
    }
}