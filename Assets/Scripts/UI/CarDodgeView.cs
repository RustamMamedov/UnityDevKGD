using UnityEngine;
using UnityEngine.UI;
using Game;
using Events;

namespace UI {
    
    public class CarDodgeView : MonoBehaviour {

        [SerializeField]
        private RawImage _carImage;

        [SerializeField]
        private Text _scoreLabel;

        [SerializeField]
        private EventListener _carDodgedEventListener;

        [SerializeField]
        private ScriptableIntValue _currentDodgedCar;

        private int _currentDodgeScore;
        private int _enemyType;

        public void Init(CarSettings carSettings) {
            _enemyType = (int) carSettings.enemyType;
            _carImage.texture = RenderManager.Instance.Render(carSettings.renderCarPrefab, carSettings.cameraPosition, carSettings.cameraRotation);
        }

        private void OnEnable() {
            _carDodgedEventListener.OnEventHappened += IncreaseScore;
        }

        private void OnDisable() {
            _carDodgedEventListener.OnEventHappened -= IncreaseScore;
        }

        private void IncreaseScore() {
            if (_enemyType == _currentDodgedCar.value) {
                _currentDodgeScore++;
                _scoreLabel.text = $"{_currentDodgeScore}";
            }
        }
    }
}