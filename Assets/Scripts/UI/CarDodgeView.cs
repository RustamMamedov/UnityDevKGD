using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game;

namespace UI {

    public class CarDodgeView : MonoBehaviour {

        [SerializeField]
        private RawImage _carImage;

        public void Init(CarSettings carSettings) {
            _carImage.texture = RenderManager.Instance.Render(carSettings.renderCarPrefab, carSettings.position);
        }
    }
}
