using UnityEngine;
using Game;

namespace Game {

    public class Headlight : MonoBehaviour {

        [SerializeField]
        private Color _gizmosColor = Color.white;

        [SerializeField]
        private CarSettings _carSettings;

        [SerializeField]
        private Light _light;

        private void Awake() {
            if (_light == null) {
                return;
            }

            _light.range = _carSettings.headlightRange * 10f;
        }

        private void OnDrawGizmos() {
            if (_carSettings == null) {
                return;
            }

            var tempMatrix = Gizmos.matrix;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawFrustum(Vector3.zero, 45f, 0f, _carSettings.headlightRange, 1f);
            Gizmos.matrix = tempMatrix;
        }
    }
}