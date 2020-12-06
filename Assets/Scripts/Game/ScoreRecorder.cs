using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

namespace Game {

    public class ScoreRecorder : MonoBehaviour {

        [SerializeField]
        private EventDispatcher _carTriggerEventDispatcher;

        [SerializeField]
        private ScriptableIntValue[] _dodgeScores;

        [SerializeField]
        private List<CarSettings> _carSettings = new List<CarSettings>();

        [SerializeField]
        private ScriptableIntValue _currentScore;

        private void OnTriggerEnter(Collider other) {
            switch (other.gameObject.name) {
                case "SUV(Clone)":
                    _dodgeScores[0].value++;
                    _currentScore.value += _carSettings[0].dodgeScore;
                    break;
                case "Truck(Clone)":
                    _dodgeScores[1].value++;
                    _currentScore.value += _carSettings[1].dodgeScore;
                    break;
                case "FamilyCar(Clone)":
                    _dodgeScores[2].value++;
                    _currentScore.value += _carSettings[2].dodgeScore;
                    break;
            }
        }
    }
}
