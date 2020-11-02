using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using Events;
namespace UI {
    public class ScoreManager : MonoBehaviour {

        [SerializeField]
        private ScriptableIntValue _currentScore;

        [SerializeField]
        private ScriptableIntValue _dodgeScore;

        [SerializeField]
        private EventListener _carDodgedEventListener;

        private void OnEnable() {
            _carDodgedEventListener.OnEventHappened += AddScore;
        }

        private void OnDisable() {
            _currentScore.value = 0;
            _carDodgedEventListener.OnEventHappened -= AddScore;
        }

        private void AddScore() {
            _currentScore.value += _dodgeScore.value;
            _dodgeScore.value = 0;
        }
    }
}
