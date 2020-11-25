using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class CarDodgeView : MonoBehaviour {

        [SerializeField]
        private RawImage _carImage;

        [SerializeField]
        private CarSettings _carSettings;
        
        [SerializeField]
        private Text _text;

        private int _family;
        private int _suv;     
        private int _truck;
        private void OnEnable() {
            Init();
        }

        private void Update() {
            if (_carSettings.FamilyCar == true) {
                _family++;
                _text.text = _family.ToString();
            }else if (_carSettings.SUV == true) {
                _suv++;
                _text.text = _suv.ToString();
            }else if(_carSettings.Truck == true) {
                _truck++;
                _text.text = _truck.ToString();
            }
        }

        public void Init() {
            _carImage.texture = RenderManager.Instance.Render(_carSettings.renderCarPrefab, _carSettings.cameraPosition, _carSettings.cameraRotation);
        }
    }
}