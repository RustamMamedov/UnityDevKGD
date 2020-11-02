using Events;
using UnityEngine;
using Values;

namespace Game {
    
    public class CountingManager : MonoBehaviour {

        // Fields.

        [SerializeField]
        private EventListener _carDodgeEventListener;

        [SerializeField]
        private ScriptableIntValue _dodgeScoreValue;

        [SerializeField]
        private ScriptableIntValue _currentScoreValue;


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
            _currentScoreValue.value += _dodgeScoreValue.value;
        }


        // Supportive methods.

        private void ResetScores() {
            _currentScoreValue.value = 0;
        }


    }

}
