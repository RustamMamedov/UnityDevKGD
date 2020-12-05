using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class CustomToggle : MonoBehaviour {

        [SerializeField]
        private Toggle _toggle;

        [SerializeField]
        private Image _leftImage;

        [SerializeField]
        private Image _rightImage;

        private void Awake() {
            _toggle.onValueChanged.AddListener((bool value) => ChangeTogglePosition(_toggle.isOn));

        }


        public void ChangeTogglePosition(bool isOn) {
            if (!isOn) {
                _leftImage.color = new Color(255, 0, 0, 0);
                _rightImage.color = new Color(0,255,0,255);
            } else {
                _leftImage.color = new Color(255, 0, 0, 255);
                _rightImage.color = new Color(0, 255, 0, 0);
            }

        }
    }
}
