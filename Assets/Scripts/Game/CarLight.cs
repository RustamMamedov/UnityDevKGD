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
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawFrustum(transform.position, 30f, _carSettings.headlightRangeh, 0f, 1.2f);
        }
    }
}