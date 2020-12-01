using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace Game {

    public class CarLight : MonoBehaviour {

        [SerializeField]
        private bool _lightCar;

        [ShowIfGroup(nameof(_lightCar))]
        [BoxGroup(nameof(_lightCar) + "/LightCar")]
        [SerializeField]
        private CarSettings _settings;

        [SerializeField]
        private Light _light;

        [BoxGroup(nameof(_lightCar) + "/LightCar")]
        [SerializeField]
        private float _constantLenghCar;

        [BoxGroup(nameof(_lightCar) + "/LightCar")]
        [SerializeField]
        private TrailRenderer _backLight;

        [HideIf(nameof(_lightCar))]
        [SerializeField]
        private ScriptableIntValue _lightSettings;

        private void OnEnable() {
            if (_lightSettings.Value!=0) {
                if (_lightCar) {
                    _light.range = _settings.lenghLightCar * _constantLenghCar;
                    _light.enabled = true;
                    _backLight.enabled = true;
                }
                else {
                    _light.enabled = false;
                }
            }
            else {
                if (!_lightCar) {
                    _light.enabled = true;
                }
                else {
                    _light.enabled = false;
                    _backLight.enabled = false;
                }
            }
        }

        private void OnDrawGizmos() {
            if (_lightCar) {
                _light.range = _settings.lenghLightCar * _constantLenghCar;
                Gizmos.matrix = transform.localToWorldMatrix;
                Gizmos.DrawFrustum(new Vector3(0f, 0f, -1f), 30f, _settings.lenghLightCar * _constantLenghCar, 1f, 1f);
            }
        }



    }
}
