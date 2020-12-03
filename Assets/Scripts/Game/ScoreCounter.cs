using Events;
using UnityEngine;

namespace Game {

    public class ScoreCounter : MonoBehaviour {

        [SerializeField]
        private EventListener _carDodgedEventListener;

        [SerializeField]
        private ScriptableIntValue _dodgeScore;

        [SerializeField]
        private ScriptableIntValue _currentScore;

        private void OnEnable() {
            _carDodgedEventListener.OnEventHappend += OnCarDodged;
        }

        private void OnDisable() {
            _currentScore.value = 0;
            _carDodgedEventListener.OnEventHappend -= OnCarDodged;
        }

        private void OnCarDodged() {
            _currentScore.value += _dodgeScore.value;
            _dodgeScore.value = 0;
        }
    }
}