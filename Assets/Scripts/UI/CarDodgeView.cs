using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game;

namespace UI {
    public class CarDodgeView : ScoreView {

        [SerializeField]
        private RawImage _carImage;

        [SerializeField]
        private CarsSettings _carSettings;

        public void Init() {
            _carImage.texture = RenderManager.Instance.Render(_carSettings);
        }
    }
}