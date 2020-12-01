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

        public RawImage carImage;

        public CarSettings carSettings;

        [SerializeField]
        private Text _scoreLabel;

        private int _score;
        private void OnEnable() {
            carSettings.dodgeScoreValue.value = 0;
            _score = 0;
            _scoreLabel.text = _score.ToString();
            _update.OnEventHappened += UpdateBehaviour;
        }

        public void UpdateBehaviour() {
            while (_score < carSettings.dodgeScoreValue.value) {
                _score++;
                _scoreLabel.text = _score.ToString();
            }
        }

    }
}
