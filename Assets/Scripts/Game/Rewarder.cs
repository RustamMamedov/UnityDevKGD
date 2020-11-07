using UnityEngine;

namespace Game {

    public class Rewarder : MonoBehaviour {

        [SerializeField]
        private ScriptableIntValue _currentScore;

        private void OnDisable() {
            _currentScore.value = 0;
        }

        public void SetScorePoints(EnemyCarToDodge enemyCar) {
            _currentScore.value += enemyCar.currentCar.GetComponent<EnemyCar>().CarSettings.dodgeScore;
        }
    }
}

