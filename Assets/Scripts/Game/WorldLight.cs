using Events;
using UnityEngine;
using Values;

namespace Game {
    
    public class WorldLight : MonoBehaviour {

        // Fields.

        [SerializeField]
        private Light _light;

        [SerializeField]
        private EventListener _lightChangedListener;

        [SerializeField]
        private ScriptableIntValue _daytimeValue;

        [SerializeField]
        private float _daylightIntensity;

        [SerializeField]
        private float _nightlightIntensity;



        // Life cycle.

        private void Start() {
            UpdateIntensity();
            _lightChangedListener.OnEventHappened += UpdateIntensity;
        }


        // Event handlers.

        private void UpdateIntensity() {
            if (_daytimeValue.value == 0) {
                _light.intensity = _daylightIntensity;
            } else {
                _light.intensity = _nightlightIntensity;
            }
        }


    }

}
