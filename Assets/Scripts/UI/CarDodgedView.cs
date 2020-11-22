using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game;

namespace UI {

    public class CarDodgedView : MonoBehaviour {

        [SerializeField]
        private RawImage _carImage;

        [SerializeField]
        private CarSettings _carSettings;

        private void Init() {
             _carImage.texture = RenderManager.Instance.Render(_carSettings);
        }

        public void OnInit() {
            Init();
        }

        //private void OnEnable() {
        //    Init();
        //}
    }
}
