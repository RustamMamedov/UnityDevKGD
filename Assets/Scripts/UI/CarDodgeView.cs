using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Events;
using Game;

namespace UI{

    public class CarDodgeView : MonoBehaviour{

        [SerializeField]
        private EventListener _update;

        [SerializeField]
        private RawImage _carImage;

        [SerializeField]
        private CarSettings _carSettings;

        [SerializeField]
        private Text _scoreLabel;

        private int _score;
        private void OnEnable() {
            _carSettings.dodgeScoreValue.value = 0;
            _score = 0;
            _scoreLabel.text = _score.ToString();
            _update.OnEventHappened += UpdateBehaviour;
            Init();
        }
        public void Init() {
            StartCoroutine(RenderManager.Instance.RenderCoroutine(_carSettings.renderCarPrefab, _carImage));
        }

        public void UpdateBehaviour() {
            while (_score < _carSettings.dodgeScoreValue.value) {
                _score++;
                _scoreLabel.text = _score.ToString();
            }
        }

    }
}
