using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game {
    public class DayOrNight : MonoBehaviour {
        [SerializeField]
        private GameObject _directionalLight;
        [SerializeField]
        private List<GameObject> _carLight;

        [SerializeField]
        private ScriptableIntValue _dayOrLight;

        private void Awake() {
            if (_dayOrLight.value == 0) {
                _directionalLight.SetActive(true);
                for (int i = 0; i < _carLight.Count; i++) {
                    _carLight[i].SetActive(false);
                }

            }
            else {
                _directionalLight.SetActive(false);
                for (int i = 0; i < _carLight.Count; i++) {
                    _carLight[i].SetActive(true);
                }
            }
        }

    }
}
