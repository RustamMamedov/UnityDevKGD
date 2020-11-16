using UnityEngine;

namespace Game {

    public class CarLight : MonoBehaviour {

        [SerializeField]
        private Color _gizmosColor = Color.white;

        [SerializeField]
        private CarSettings _carSettings;

        private void OnDrawGizmos() {
            if (_carSettings == null) {
                return;
            }

            Gizmos.color = _gizmosColor;
            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
            Gizmos.DrawFrustum(Vector3.zero, 30f, 0f, _carSettings.headlightRange, 1f);
        }
    }
}