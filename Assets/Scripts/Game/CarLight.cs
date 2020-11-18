using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class CarLight : MonoBehaviour {

        [SerializeField]
        private Color _gizmosColor;

        [SerializeField]
        private PlayerCar _playerCar;

        private void OnDrawGizmos() {

        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = _gizmosColor;
            Vector3 position = new Vector3(0f, 0f, 0f);
            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
            Gizmos.DrawFrustum(position, 25f, _playerCar.CarSettings.carLightDistance, 0f, 1.5f);
        }
    }
}