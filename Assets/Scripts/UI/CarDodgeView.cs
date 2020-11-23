using UnityEngine;
using UnityEngine.UI;
using Game;

namespace UI {

    public class CarDodgeView : MonoBehaviour {

        [SerializeField]
        private RawImage _carImage;

        private CarSettings _carSettings;

        public void SetCarSettings(CarSettings settings) {
            _carSettings = settings;
        }

        public void Init() {
            _carImage.texture = RenderManager.Instance.Render(
                _carSettings.renderCarPrefab, 
                _carSettings.position, 
                _carSettings.rotation
            );
        }
    }
}