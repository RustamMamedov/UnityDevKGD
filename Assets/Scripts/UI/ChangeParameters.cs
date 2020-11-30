﻿using UnityEngine;
using UnityEngine.UI;
using Game;

namespace UI {

    public class ChangeParameters : MonoBehaviour {

        [SerializeField]
        private Slider _sliderVolume;

        [SerializeField]
        private ScriptableFloatValue _volume;

        [SerializeField]
        private Slider _sliderDifficulty;

        [SerializeField]
        private ScriptableIntValue _difficulty;

        [SerializeField]
        private Slider _sliderTimeLight;

        [SerializeField]
        private ScriptableIntValue _timeLight;

        [SerializeField]
        private Button _oKButton;        
        
        [SerializeField]
        private Button _cancelButton;

        public void SaveChange() {
            _volume.value = _sliderVolume.value;
            _difficulty.value = (int)_sliderDifficulty.value;
            _timeLight.value = (int)_sliderTimeLight.value;
        }

        public void RecoveryChange() {
            _sliderVolume.value = _volume.value;
            _sliderDifficulty.value = _difficulty.value; 
            _sliderTimeLight.value = _timeLight.value;
        }

        private void Awake() {
            _oKButton.onClick.AddListener(SaveChange);
            _cancelButton.onClick.AddListener(RecoveryChange);
        }
    }
}