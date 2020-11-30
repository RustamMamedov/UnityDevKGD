using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class ToggleScript : MonoBehaviour {
        
        [SerializeField]
        private Toggle _toggle;

        [SerializeField]
        private Image _leftImage;

        [SerializeField]
        private Image _rightImage;

        private void Awake() {
            _toggle.onValueChanged.AddListener(ChangeTogglePosition);
            ChangeTogglePosition(false);
        }

        private void ChangeTogglePosition(bool isOn) {
            var color = new Color(164, 161, 161,255);
            if(isOn) {
                _leftImage.color = color;
                _rightImage.color = Color.blue;
            }
            else {
                _leftImage.color = Color.blue;
                _rightImage.color = color;
            }
        }
    }

}
