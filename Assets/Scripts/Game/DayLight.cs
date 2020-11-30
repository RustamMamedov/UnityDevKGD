using System.Collections;
using Events;
using UnityEngine;

namespace Game {

    public class DayLight : MonoBehaviour {

        [SerializeField]
        private GameObject _globalLight;

        [SerializeField]
        private ScriptableIntValue _timeCurrent;

        private void Awake() {
            if (_timeCurrent.value == 1) {
                _globalLight.SetActive(false);
            }
            else {
                _globalLight.SetActive(true);
            }
        }
    }

}
