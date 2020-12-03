using UnityEngine;
using UnityEngine.UI;
using Events;
using Game;

namespace UI {

    public class CarDodgeView : MonoBehaviour {

        [SerializeField]
        private RawImage _carImage;

        [SerializeField]
        private EventListener _carDodgeEventListener;

        [SerializeField]
        private ScriptableIntValue _typePassedCar;

        [SerializeField]
        private Text _dodgeScoreLabel;

        private CarSettings _carSettings;
        private int _dodgeScore;

        private void Awake() {
            _carDodgeEventListener.OnEventHappend += IncreaseDodgeScore;
        }

        private void OnEnable() {
            _dodgeScore = 0;
            _dodgeScoreLabel.text = $"{_dodgeScore}";
        }

        private void IncreaseDodgeScore() {
            if (_typePassedCar.value == (int)_carSettings.enemyType) {
                _dodgeScore++;
                _dodgeScoreLabel.text = $"{_dodgeScore}";
            }
        }

        public void SetCarSettings(CarSettings settings) {
            _carSettings = settings;
        }

        public void Init() {
            _carImage.texture = RenderManager.Instance.Render(
                _carSettings.renderCarPrefab,
                _carSettings.position,
                _carSettings.rotation
            );
        }
    }
}