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
        private Text _scoreLabel;

        private int _score = 0;
        
        public void Init(GameObject renderCarPrefab, Vector3 renderCameraPosition, Quaternion renderCameraRotation) {
           _carImage.texture = RenderManager.Instance.CarRender(renderCarPrefab,renderCameraPosition, renderCameraRotation); 
        }

        public void DodgeCounter() {
            _score++;
            _scoreLabel.text = $"{_score}";
        }

        private void OnDisable() {
            _score = 0;
            _scoreLabel.text = $"{_score}";
        }
    }
}
