using System.Collections;
using Events;
using UnityEngine;

namespace Game {

    public class DayLight : MonoBehaviour {

        [SerializeField]
        private GameObject _globalLight;

        [SerializeField]
        private ScriptableIntValue _timeCurrent;

        [SerializeField]
        private EventListener _update;

        private void OnEnable() {
            _update.OnEventHappened += UpdateBehaviour;
        }

        private void UpdateBehaviour() {
            DayLightActivity();
        }

        private void DayLightActivity() {
            if (_timeCurrent.value == 1) {
                _globalLight.SetActive(false);
            }
            else {
                _globalLight.SetActive(true);
            }
        }
    }

}
