using UnityEngine;

namespace Game {

    public class CarLight : MonoBehaviour {

        [SerializeField]
        private Color _colorLight;

        [SerializeField]
        private CarSettings _carSettings;

        private Light _lightCar;
        private int _rangeLightCarRatio = 8;

        private void Awake() {
            _lightCar = GetComponent<Light>();
            _lightCar.range = _carSettings.lightDistance * _rangeLightCarRatio;
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = _colorLight;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawFrustum(transform.position, 45f, 0f, _carSettings.lightDistance, 2f);
        }
    }
}
