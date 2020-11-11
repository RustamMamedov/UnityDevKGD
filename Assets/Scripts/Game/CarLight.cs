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
            //Debug.Log(transform.position);
            Gizmos.DrawFrustum(transform.position, 45f, _carSettings.carLightLength, 0.5f, 1f);
        }
    }

}