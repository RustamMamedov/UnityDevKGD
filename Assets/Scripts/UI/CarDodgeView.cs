using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class CarDodgeView : MonoBehaviour {

        [SerializeField]
        private RawImage _carImage;

        public void Init(CarSettings settings) {
            _carImage.texture = RenderManager.Instance.Render(settings.renderCarPrefab, settings.renderCameraPos, settings.renderCameraRot);
        }
    }
}