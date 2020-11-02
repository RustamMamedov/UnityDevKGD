using System.Collections.Generic;
using Game;
using UnityEngine;

namespace UI {

    public class ScoreManager : MonoBehaviour {

        [SerializeField] private List<EnemyCar> _enemyCars = new List<EnemyCar>();

        [SerializeField] private ScriptableIntValue _currentScore;

        private void OnTriggerEnter(Collider other) {
            for (int i = 0; i < _enemyCars.Count; i++) {
                if (_enemyCars[i].tag.Equals(other.tag)) {
                    _currentScore.value += _enemyCars[i].DodgeScore.dodgeScore;
                }
            }
        }
    }
}