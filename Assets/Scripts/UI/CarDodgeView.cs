using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game;

namespace UI {

    public class CarDodgeView : MonoBehaviour {

        [SerializeField]
        private RawImage _carImage;

        [SerializeField]
        public CarSettings _carSettings;

        [SerializeField]
        private Text _scoreText;

        private int _score;

        public void Init() {
            _carImage.texture = RenderManager.Instance.Render(_carSettings.renderCarPrefab, _carSettings.cameraPosition, _carSettings.cameraRotation);
        }

        private void Update() {
            ScoreShow();
        }

        public void ScoreShow() {
            _score = _carSettings.currentDodgeScore;
            _scoreText.text = _score.ToString();
        }

    }
}

