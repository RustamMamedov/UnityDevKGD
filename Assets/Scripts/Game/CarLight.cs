using UnityEngine;
using Game;

namespace GameEditor {
    public class CarLight : MonoBehaviour {

        [SerializeField]
        private Color _gizmosColor;

        [SerializeField]
        private CarSettings _carSettings;
    
        private void OnDrawGizmos() {
            var tempMatrix = Gizmos.matrix;
            Gizmos.color = _gizmosColor;
            Gizmos.matrix = gameObject.transform.localToWorldMatrix;
            Gizmos.DrawFrustum(Vector3.zero, 15f,_carSettings.lightLength,0.1f,0.5f);
            Gizmos.matrix = tempMatrix;
        }
    }
}
