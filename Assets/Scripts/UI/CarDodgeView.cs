using System.Collections;
using Events;
using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class CarDodgeView : MonoBehaviour {

        [SerializeField]
        private RawImage _carImage;

        [SerializeField]
        private CarSettings _carSettings;

        [SerializeField]
        private Text _scoreLabel;

        private float _scoreCountDelay = 0.1f;

        public int currentScore = 0;

        private void OnEnable() {
            _scoreLabel.text = currentScore.ToString();
        }

        private void OnDisable() {
            currentScore = 0;
        }

        public void Init() {
            RenderManager.Instance.SetCameraTransform(_carSettings.cameraPosition, _carSettings.cameraRotation);
            _carImage.texture = RenderManager.Instance.Render(_carSettings.renderCarPrefab);
        }

        public void SetScore(int value) {
            var score = SetScoreCoroutine(value);
            StartCoroutine(score);
        }

        private IEnumerator SetScoreCoroutine(int newScore) {
            while (currentScore != newScore) {
                currentScore++;
                _scoreLabel.text = currentScore.ToString();
                yield return new WaitForSeconds(_scoreCountDelay);
            }
        }
    }
}
