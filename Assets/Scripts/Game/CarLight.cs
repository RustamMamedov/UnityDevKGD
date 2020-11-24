using UnityEngine;

namespace Game {
    
    public class CarLight : MonoBehaviour {
        
        [SerializeField]
        private Color _gizmosColor = Color.white;
        
        [SerializeField]
        private CarSettings _playerCarSettings;

        [SerializeField]
        private Light _carLight;

        private void Awake() {
            _carLight.range = _playerCarSettings.carLightLength;
        }
        private void OnDrawGizmosSelected() {
            var tempMatrix = Gizmos.matrix;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawFrustum(new Vector3(0f, 0f, -1f), 45f, _playerCarSettings.carLightLength, 1f, 1f);
            Gizmos.matrix = tempMatrix;
        }
    }

}