using UnityEngine;

namespace Game {

    public class CarLight : MonoBehaviour {

        [SerializeField]
        private CarSettings _carSettings;

        [SerializeField]
        private Color _color;

        private float _offset = 2f;

        private void OnDrawGizmos() {
            Gizmos.color = _color;
            //Matrix4x4 temp = Gizmos.matrix;
            //Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawFrustum(Vector3.zero, 45f, _carSettings.lightDistance + _offset, 0f, 2f);

            //Gizmos.DrawWireSphere(Vector3.zero, 10f);
            //Gizmos.matrix = temp;
            
        }
    }
}

