using UnityEngine;
using System;

namespace Game {

    public class SettingsManager : MonoBehaviour {

        [SerializeField]
        private ScriptableFloatValue _difficulty;

        [SerializeField]
        private ScriptableFloatValue _daytime;

        [SerializeField]
        private GameObject _playerCar;

        [SerializeField]
        private Light _sunLight;

        [SerializeField]
        private Color _daySunColor;

        [SerializeField]
        private Color _nightSunColor;

        [SerializeField]
        private EnemySpawner _enemySpawner;

        private void Awake() {
            ApplySettings();
        }

        private void ApplySettings() {
            bool boolDaylightValue = !Convert.ToBoolean(_daytime.value);

            if (boolDaylightValue) {
                _sunLight.color = _daySunColor;
            } else {
                _sunLight.color = _nightSunColor;
            }
            _playerCar.GetComponent<PlayerCar>().SetLightingState(boolDaylightValue);

            _enemySpawner.SetDifficulty(_difficulty.value);

        }
    }
}