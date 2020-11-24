using Events;
using Game;
using System.Linq;
using UnityEngine;
using Values;

namespace Managers {

    public class CountingManager : MonoBehaviour {

        // Fields.

        [SerializeField]
        private EventListener _carDodgeEventListener;

        [SerializeField]
        private ScriptableCarSettingsReference _dodgedCarReference;

        [SerializeField]
        private ScriptableIntValue _currentScoreValue;

        [SerializeField]
        private CarSettings[] _managedCars;


        // Life cycle.

        private void Awake() {
            ResetScores();
        }

        private void OnEnable() {
            _carDodgeEventListener.OnEventHappened += OnCarDodge;
        }

        private void OnDisable() {
            _carDodgeEventListener.OnEventHappened -= OnCarDodge;
        }

        private void OnDestroy() {
            ResetScores();
        }


        // Event handlers.

        private void OnCarDodge() {
            var carSettings = _dodgedCarReference.reference;
            if (_managedCars.Contains(carSettings)) {
                _currentScoreValue.value += carSettings.dodgeScore;
                carSettings.dodgesCountValue.value += 1;
            }
        }


        // Supportive methods.

        private void ResetScores() {
            _currentScoreValue.value = 0;
            foreach (var carSettings in _managedCars) {
                carSettings.dodgesCountValue.value = 0;
            }
        }


    }

}
