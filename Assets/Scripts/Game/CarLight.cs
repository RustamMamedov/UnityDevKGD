using UnityEngine;

namespace Game {
    public class CarLight : MonoBehaviour {

        [SerializeField]
        private Color _gizmosColor = Color.white;

        [SerializeField]
        private CarSettings _carSettings;

        [SerializeField]
        private Light _light;

        private void OnDrawGizmosSelected() {
            Gizmos.color = _gizmosColor;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = _gizmosColor;
            Gizmos.DrawFrustum(Vector3.zero, 20f, _carSettings.carLightLenght, 0.5f, 3f);
        }

        private void Awake() {
            _light.range = _carSettings.carLightLenght;
        }

    }
}
