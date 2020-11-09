using Events;
using UnityEngine;

namespace Game {

    public class RewardHandle : MonoBehaviour {

        [SerializeField]
        private ScriptableIntValue _currentScore;

        [SerializeField]
        private EventListener _carDodgeEventListener;

        [SerializeField]
        private Cars _dodgedCars;

        public void OnCarDodged() {
            _dodgedCars.carsList[0].TryGetComponent<EnemyCar>(out var enemyCar);
            _currentScore.value += enemyCar.carSettings.dodgeScore;
            _dodgedCars.carsList.RemoveAt(0);
        }

        private void OnEnable() {
            _carDodgeEventListener.OnEventHappened += OnCarDodged;
        }

        private void OnDisable() {
            _dodgedCars.carsList.Clear();
            _currentScore.value = 0;
            _carDodgeEventListener.OnEventHappened -= OnCarDodged;
        }

    }
}
