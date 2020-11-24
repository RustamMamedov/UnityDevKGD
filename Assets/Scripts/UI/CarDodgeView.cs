using Game;
using System.Collections;
using System.Collections.Generic;
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

        private int _dogedCarCounter;

        public void Init(GameObject carPrefab, Vector3 cameraPosition, Quaternion cameraRotation) {
            _carImage.texture = RenderManager.Instance.Render(_carSettings.renderCarPrefab, _carSettings.cameraPosition, _carSettings.cameraRotation);
        }
        public void CheckDodgeId(ScriptableIntValue currentId) {
            if (_carSettings.dodgeCarId == currentId.value) {
                _dogedCarCounter++;
                _scoreLabel.text = $"{_dogedCarCounter}";
            }
        }

        public void ClearScoreLabel() {
            _dogedCarCounter = 0;
            _scoreLabel.text = $"{_dogedCarCounter}";
        }
    }
}
