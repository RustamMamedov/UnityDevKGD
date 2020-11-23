using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game;
using Events;

namespace UI {

    public class CarDodgedView : MonoBehaviour {

        [SerializeField]
        private RawImage _carImage;

        [SerializeField]
        private CarSettings _carSettings;

        [SerializeField]
        private ScriptableIntValue _currentCountDodged;

        [SerializeField]
        private Text _lable;

        private void Init() {
             _carImage.texture = RenderManager.Instance.Render(_carSettings);
        }

        public void OnInit() {
            Init();
        }

        public void SummCountDodged() {
            _lable.text=$"{_currentCountDodged.Value}";
        }

        private void OnEnable() {
            _currentCountDodged.Value = 0;
            _lable.text = $"{0}";
        }

        //private void OnEnable() {
        //    Init();
        //}
    }
}
