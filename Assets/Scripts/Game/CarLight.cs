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
            Gizmos.DrawFrustum(transform.position, 20f, _carSettings.carLightLenght, 0.5f, 3f);
        }
    }
}
