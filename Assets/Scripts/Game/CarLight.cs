using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game {

    public class CarLight : MonoBehaviour {

        [SerializeField]
        private Color _color = Color.white;

        [SerializeField]
        private CarSettings _carSettings;


        private void OnDrawGizmos() {
            Gizmos.color = _color;

            Gizmos.DrawFrustum(transform.position, 25f, _carSettings.lightDistance, 0.5f, 2f);
        }



    }
}
