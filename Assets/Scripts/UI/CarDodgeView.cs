using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class CarDodgeView : MonoBehaviour {

        [SerializeField]
        private RawImage _carImage;

        [SerializeField]
        private CarSettings _carSettings;

        public void Init() {
            RenderManager.Instance.SetCameraTransform(_carSettings.cameraPosition, _carSettings.cameraRotation);
            _carImage.texture = RenderManager.Instance.Render(_carSettings.renderCarPrefab);
        }
    }
}
