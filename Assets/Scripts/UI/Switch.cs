using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Audio;
using Game;

namespace UI {
    public class Switch : MonoBehaviour {

        [SerializeField]
        private GameObject _switchImageOn;

        [SerializeField]
        private GameObject _switchImageOff;

        [SerializeField]
        private Button _switchButton;

        [SerializeField]
        private ScriptableIntValue _modeValue;


        private void Awake() {
            MoveImage(_modeValue.value);
            _switchButton.onClick.AddListener(SwitchMode);
        }

        public void SwitchMode() {
            if (_modeValue.value == 1) {
                _modeValue.value = 0;
            }
            else {
                _modeValue.value = 1;
            }
            MoveImage(_modeValue.value);
        }
        private void MoveImage(int val) {
            if (val==1) {
                _switchImageOff.SetActive(true);
                _switchImageOn.SetActive(false);
            }
            else {
                _switchImageOff.SetActive(false);
                _switchImageOn.SetActive(true);
            }
        }
   
    }
}