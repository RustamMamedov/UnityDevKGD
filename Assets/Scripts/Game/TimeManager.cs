using UnityEngine;

namespace Game {

    public class TimeManager : MonoBehaviour {

        [SerializeField]
        private Light _upperLight;

        [SerializeField]
        private Light _leftLight;

        [SerializeField]
        private Light _rightLight;

        [SerializeField]
        private Light _sunLight;

        private void OnEnable() {
            switch (Save.Settings.time) {
                case Save.SavedSettings.Time.Day:
                    _upperLight.enabled = false;
                    _leftLight.enabled = false;
                    _rightLight.enabled = false;
                    _sunLight.intensity = 0.7f;
                    break;
                case Save.SavedSettings.Time.Night:
                    _upperLight.enabled = true;
                    _leftLight.enabled = true;
                    _rightLight.enabled = true;
                    _sunLight.intensity = 0.05f;
                    break;
            }
        }
    }
}
