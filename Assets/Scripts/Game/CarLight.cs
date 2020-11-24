using UnityEngine;

namespace Game {
    
    public class CarLight : MonoBehaviour {

        [SerializeField]
        private Car _car;

        [SerializeField]
        private Light _light;

        private void Awake() {
            _light.range = _car.CarSettings.carLightDistance;
        }

#if UNITY_EDITOR

        private void OnDrawGizmosSelected() {
            if (_car == null) {
                return;
            }

            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
            Gizmos.DrawFrustum(
                center: Vector3.zero,
                fov: 20,
                maxRange: _car.CarSettings.carLightDistance,
                minRange: Mathf.Min(0.5f, _car.CarSettings.carLightDistance),
                aspect: 1
            );

        }

#endif


    }

}
