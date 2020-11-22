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

        private int score = 0;
        
        public void Init(GameObject renderCarPrefab, Vector3 renderCameraPosition, Quaternion renderCameraRotation) {
           _carImage.texture = RenderManager.Instance.CarRender(renderCarPrefab,renderCameraPosition, renderCameraRotation); 
        }

        public void DodgeCounter() {
            score++;
            _scoreLabel.text = $"{score}";
        }

        private void OnDisable() {
            score = 0;
            _scoreLabel.text = $"{score}";
        }
    }
}
