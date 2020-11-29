using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    
    public class DayTime : MonoBehaviour {

        [SerializeField] 
        private ScriptableBoolValue _nightSetting;

        [SerializeField] 
        private GameObject _worldLight;

        [SerializeField] 
        private GameObject _carLights;

        private void Awake() {
            if (_nightSetting.value) {
                SetNight();
            } else {
                SetDay();
            }
        }

        private void SetDay() {
            _carLights.SetActive(false);
            _worldLight.SetActive(true);
        }

        private void SetNight() {
            _carLights.SetActive(true);
            _worldLight.SetActive(false);
        }
    }
}


