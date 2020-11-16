using UnityEngine;

namespace Game {

    public class CarLight : MonoBehaviour {

        [SerializeField]
        private Color _colorLight;

        [SerializeField]
        private CarSettings _carSettings;

        private void OnDrawGizmosSelected() {
            Gizmos.color = _colorLight;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawFrustum(transform.position, 45f, 0f, _carSettings.lightDistance, 2f);
        }
    }
}
