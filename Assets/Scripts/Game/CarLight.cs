using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game {
    public class CarLight : MonoBehaviour {
        [SerializeField]
        private CarSettings carSettings;

        private void OnDrawGizmos() {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawFrustum(transform.position, 45f, 0.1f, carSettings.lenghtLight,1f);
           
        }
    }
}