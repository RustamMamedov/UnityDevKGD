﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class CarLight : MonoBehaviour {

        [SerializeField]
        private Car _car;

        [SerializeField]
        private Light _light;

        private void OnDrawGizmos() {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawFrustum(new Vector3(0f, 0f, -1f), 45f, _car.CarSettings.lightlenght, 1f, 1f);
            _light.range = _car.CarSettings.lightlenght*20f;
        }
    }
}

