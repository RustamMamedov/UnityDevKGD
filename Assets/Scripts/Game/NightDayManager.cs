using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class NightDayManager : MonoBehaviour {

        [SerializeField]
        private GameObject _leftLight;

        [SerializeField]
        private GameObject _rightLight;

        [SerializeField]
        private GameObject _light;

        [SerializeField]
        private ScriptableBoolValue _isDay;


        private void OnEnable() {
            if (_isDay.value) {
                _leftLight.SetActive(false);
                _rightLight.SetActive(false);
                _light.SetActive(true);
            }  else {
                _leftLight.SetActive(true);
                _rightLight.SetActive(true);
                _light.SetActive(false);
            }
        }
    }
}