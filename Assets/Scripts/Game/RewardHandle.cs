using Events;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Game {

    public class RewardHandle : MonoBehaviour {

        [SerializeField]
        private ScriptableIntValue _currentScore;

        [SerializeField]
        private EventListener _carDodgeEventListener;

        [SerializeField]
        private Cars _dodgedCars;

        [SerializeField]
        private CarDodgeView _familyCarDodgeView;

        [SerializeField]
        private CarDodgeView _suvDodgeView;

        [SerializeField]
        private CarDodgeView _truckDodgeView;

        public void OnCarDodged() {
            _dodgedCars.carsList[0].TryGetComponent<EnemyCar>(out var enemyCar);
            _currentScore.value += enemyCar.carSettings.dodgeScore;
            SetupScore(enemyCar.CarSettings);
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

        private void SetupScore(CarSettings carSettings) {
            switch (carSettings.carType) {
                case CarSettings.CarType.FamilyCar:
                    _familyCarDodgeView.SetScore(_familyCarDodgeView.currentScore + 1);
                    break;
                case CarSettings.CarType.SUV:
                    _suvDodgeView.SetScore(_suvDodgeView.currentScore + 1);
                    break;
                case CarSettings.CarType.Truck:
                    _truckDodgeView.SetScore(_truckDodgeView.currentScore + 1);
                    break;
            }
        }
    }
}
