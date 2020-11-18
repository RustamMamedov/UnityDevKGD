using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class CarLight : MonoBehaviour {

        [SerializeField]
        private Car _car;

        private void OnDrawGizmosSelected() {
            Gizmos.DrawFrustum(transform.position, 30f, 7f, _car.CarSettings.lightDistance, .5f);
        }
    }
}