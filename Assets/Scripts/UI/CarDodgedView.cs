using UnityEngine;
using UnityEngine.UI;
using Game;

namespace UI {

    public class CarDodgedView : MonoBehaviour {

        [SerializeField]
        private RawImage _carImage;

        [SerializeField]
        private CarSettings _carSettings;

        public void Init(GameObject renderCarPrefs, Vector3 renderCameraPos) {
            _carImage.texture = RenderManager.Instance.Render(_carSettings.renderCarPrefab,_carSettings.renderCameraPosition);
        }
    }
}

