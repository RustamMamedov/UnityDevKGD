using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class AutoDodge : MonoBehaviour {

        [SerializeField]
        private BoxCollider _detectCollider;

        [SerializeField]
        private float _distanceForDodge;

        private void OnEnable() {
            _detectCollider.center = new Vector3(0, 0, _distanceForDodge / 2);
            _detectCollider.size = new Vector3(0, 0, _distanceForDodge);
        }
    }
}
