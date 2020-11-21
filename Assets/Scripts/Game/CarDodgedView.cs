using UnityEngine;
using UnityEngine.UI;
using Game;

namespace UI {

    public class CarDodgedView : MonoBehaviour {

        [SerializeField]
        private RawImage _carImage;

        [SerializeField]
        private CarSettings _carSettings;

        private void OnEnable() {
            Init();
        }

        public void Init() {
            _carImage.texture = RenderManager.Instanse.Render(_carSettings);
         }
    }
}