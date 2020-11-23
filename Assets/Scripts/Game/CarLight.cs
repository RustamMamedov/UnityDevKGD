using UnityEngine;

namespace Game {

    public class CarLight : MonoBehaviour {

        [SerializeField]
        private CarSettings _carSettings;

        [SerializeField]
        private Color _gizmosColor = Color.red;

        [SerializeField]
        private Light _light;

        private float multyplier = 10f;

        private void Awake() {
            _light.range = _carSettings.lightRange * multyplier;
        }
        private void OnDrawGizmos() {

            Gizmos.color = _gizmosColor;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawFrustum(Vector3.zero, 45f, _carSettings.lightRange, 0f, 2f);
        }
    }
}

