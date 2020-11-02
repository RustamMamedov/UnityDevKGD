using System.Collections.Generic;
using Game;
using UnityEngine;

namespace UI {

    public class ScoreManager : MonoBehaviour {

        [SerializeField] private List<EnemyCar> _enemyCars;

        [SerializeField] private ScriptableIntValue _currentScore;

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Family")) {
                _currentScore.value += _enemyCars[0]._dodgeScore.dodgeScore;
            }

            if (other.CompareTag("SUV")) {
                _currentScore.value += _enemyCars[1]._dodgeScore.dodgeScore;
            }

            if (other.CompareTag("Truck")) {
                _currentScore.value += _enemyCars[2]._dodgeScore.dodgeScore;
            }
        }
    }
}