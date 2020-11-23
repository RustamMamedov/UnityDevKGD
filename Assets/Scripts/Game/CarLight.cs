using System.Collections;
using System.Collections.Generic;
using UnityEngine; 


namespace Game {
    public class CarLight : MonoBehaviour {
        [SerializeField]
        private CarSettings carSettings;

        [SerializeField]
        private List<Light> _lights;

        private void OnEnable() {
            _lights[0].range = carSettings.lenghtLight*15;
            _lights[1].range = carSettings.lenghtLight*15;
        }
    }
}