﻿using UnityEngine;

namespace Game {

    public class CarLight : MonoBehaviour {

        [SerializeField]
        private CarSettings _carSettings;

        [SerializeField]
        private Color _color;

        private float _offset = 2f;

        private void OnDrawGizmos() {
            Gizmos.color = _color;
            Gizmos.DrawFrustum(transform.position, 45f, _carSettings.lightDistance + _offset, 0f, 2f);
        }
    }
}
