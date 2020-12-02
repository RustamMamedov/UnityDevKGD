using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Audio;

namespace UI {
    public class Switch : MonoBehaviour, IPointerDownHandler {

        [SerializeField]
        private GameObject _switchImageOn;

        [SerializeField]
        private GameObject _switchImageOff;

        /*[SerializeField]
        private AudioSourcePlayer _audio;*/

        public delegate void Change(bool val);
        public event Change change;

        private bool _isOn=true;
        public  bool isOn=> _mode;
        
        private bool _mode=true;

        private void OnEnable() {
            SwitchMode(_isOn);
            if (!_mode) {
                SwitchMode(_isOn);
                _mode = true;
            }
        }

        public void SwitchMode(bool val) {
            if (val != _isOn) {
                _isOn = val;
                MoveImage(_isOn);
               // _audio.Play();
                if (change != null) {
                    change(_isOn);
                }
                _mode = !_mode;
            }
        }
        private void MoveImage(bool val) {
            if (val) {
                _switchImageOff.SetActive(true);
                _switchImageOn.SetActive(false);
            }
            else {
                _switchImageOff.SetActive(false);
                _switchImageOn.SetActive(true);
            }
        }
        public void OnPointerDown(PointerEventData eventData) {
            SwitchMode(!_isOn);
        }
    }
}