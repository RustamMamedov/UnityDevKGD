using UnityEngine;
using UnityEngine.UI;

namespace UI {
    
    public class CarDodgeView : MonoBehaviour {

        [SerializeField]
        private RawImage _carImage;

        public void Init(GameObject prefab, Vector3 cameraPosition, Quaternion cameraRotation) {
            _carImage.texture = RenderManager.Instance.Render(prefab, cameraPosition, cameraRotation);
        }
    }
}