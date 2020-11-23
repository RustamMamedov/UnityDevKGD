using UnityEngine;

namespace Game {

    public class CarLight : MonoBehaviour {

        [SerializeField]
        private CarSettings _carSettings;

        [SerializeField]
        private Color _color;

        [SerializeField]
        private Light _light;

        private float _offset = 2f;

        private void Awake() {
            _light.range = _carSettings.lightDistance * _offset;
        }

        private void OnDrawGizmos() {
            Gizmos.color = _color;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawFrustum(Vector3.zero, 45f, _carSettings.lightDistance + _offset, 0f, 2f);
        }
    }
}

