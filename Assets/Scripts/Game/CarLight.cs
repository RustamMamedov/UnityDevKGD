using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    public class CarLight : MonoBehaviour {

        [SerializeField]
        private Color _gizmosColor = Color.white;

        [SerializeField]
        private CarSettings _carSettings;

        private void OnDrawGizmos() {
            Gizmos.color = _gizmosColor;
            Gizmos.DrawFrustum(transform.position, 90f, _carSettings.lightLength, 0f, .5f);
        }
    }
}
