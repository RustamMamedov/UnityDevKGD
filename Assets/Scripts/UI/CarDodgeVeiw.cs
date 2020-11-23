using Game;
using UnityEngine;
using UnityEngine.UI;
using Events;

namespace UI {

    public class CarDodgeVeiw : MonoBehaviour {
        
        [SerializeField]
        private RawImage _carImage;

        [SerializeField]
        private CarSettings _carSettings;

        [SerializeField]
        private Text _scoreLabel;

        [SerializeField] 
        private EventListener _carDodgedEventListener;

        [SerializeField]
        private ScriptableIntValue _currentScore;

        private int _scoreSaver = 0;

        private int score = 0;

        private void OnEnable() {
            SupscribeToEvent();
            Init();
        }

         private void OnDisable() {
            UnsubscribeFromEvent();

            score = 0;
            _scoreLabel.text = $"{score}";
        }

        private void SupscribeToEvent() {
            _carDodgedEventListener.OnEventHappened += AddScore;
        }

        private void UnsubscribeFromEvent() {
            _carDodgedEventListener.OnEventHappened -= AddScore;
        }

        public void Init() {
            _carImage.texture = RenderManager.Instance.Render(_carSettings.renderCarPrefab, _carSettings.renderCameraPosition, _carSettings.renderCameraRotation);
        }

        private void AddScore() {
            var changeOfScore = _currentScore.value - _scoreSaver;
            _scoreSaver = _currentScore.value;

            if (changeOfScore == _carSettings.dodgeScore ){
                score++;
                _scoreLabel.text = $"{score}";
            }

        }

    }
}