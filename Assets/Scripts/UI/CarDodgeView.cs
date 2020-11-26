using UnityEngine;
using UnityEngine.UI;
using Game;

namespace UI {
    public class CarDodgeView : MonoBehaviour {

        [SerializeField]
        private RawImage _carImage;

        [SerializeField]
        private CarSettings _carSettings;

        private void OnEnable() {
            Init();
        }
        public void Init() {
          
                _carImage.texture = RenderManager.Instance.Render(_carSettings.renderCarPrefab);
            
        }
    }
}