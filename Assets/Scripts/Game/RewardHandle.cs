using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class RewardHandle : MonoBehaviour {

        [SerializeField]
        private ScriptableIntValue _currentScore;

        [SerializeField]
        private Cars _dodgedCars;

        public void GetPointsForCar(GameObject car) {
            _currentScore.value += car.GetComponent<EnemyCar>().carSettings.dodgeScore;
            _dodgedCars.carsList.RemoveAt(0);
        }

        private void OnDisable() {
            _dodgedCars.carsList.Clear();
            _currentScore.value = 0;
        }

    }
}
