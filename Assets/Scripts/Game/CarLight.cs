using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class CarLight : MonoBehaviour {
        
        [SerializeField] 
        private CarSettings _carSettings; 

        [SerializeField]
        private Light _carLight;  

        private void Awake() {
            _carLight.range = _carSettings.lightLength;
        }
    }
}