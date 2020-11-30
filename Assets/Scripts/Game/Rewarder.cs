using UnityEngine;
using Events;

namespace Game {

    public class Rewarder : MonoBehaviour {

        [SerializeField]
        private ScriptableIntValue _currentScore;

        [SerializeField]
        private ScriptableIntValue _scoreToAdd;

        [SerializeField]
        private EventListener _carDodgeListener;

        private void OnEnable() {
            _carDodgeListener.OnEventHappened += SetScorePoints;
        }

        private void OnDisable() {
            _carDodgeListener.OnEventHappened -= SetScorePoints;
            _currentScore.value = 0;
        }

        public void SetScorePoints() {
            _currentScore.value += _scoreToAdd.value;
        }
    }
}

