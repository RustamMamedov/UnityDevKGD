using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Game {
    public class DayNight : MonoBehaviour {

        [SerializeField]
        private GameObject[] _lightCar;

        [SerializeField]
        private GameObject _lightDir;

        [SerializeField]
        private ScriptableIntValue _dayOrNight;

        private void Awake() {
            if (_dayOrNight) {
                _lightCar[0].SetActive(false);
                _lightCar[1].SetActive(false);
                _lightDir.SetActive(true);
            } else {
                _lightCar[0].SetActive(true);
                _lightCar[1].SetActive(true);
                _lightDir.SetActive(false);
            }
        }
    }
}