using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class CarDodgeVeiw : MonoBehaviour {
        
        [SerializeField]
        private RawImage _carImage;

        [SerializeField]
        private CarSettings _carSettings;

        private void OnEnable() {
            Init();
        }

        public void Init() {
            _carImage.texture = RenderManager.Instance.Render(_carSettings.renderCarPrefab, _carSettings.renderCameraPosition, _carSettings.renderCameraRotation);
        }

    }
}