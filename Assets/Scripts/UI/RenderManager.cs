using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

namespace UI {

    public class RenderManager : MonoBehaviour {

        public static RenderManager Instance;

        [SerializeField]
        private Camera _renderCamera;

        [SerializeField]
        private Transform _rootTransform;

        [SerializeField]
        private Light _light;

        private RenderTexture _texture;

        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        public RenderTexture Render(CarsSettings carsSettings) {
            var carInstanse = Instantiate(carsSettings.renderCarPrefab, _rootTransform);
            _texture = RenderTexture.GetTemporary(64, 64, 16);
            _texture.antiAliasing = 8;
            _texture.Create();
            _renderCamera.transform.position = _rootTransform.position + carsSettings.positionCamera;
            _renderCamera.transform.rotation = Quaternion.Euler(carsSettings.rotationCamera);
            _renderCamera.targetTexture = _texture;
            _light.enabled = true;
            _renderCamera.Render();
            _light.enabled = false;
            _renderCamera.targetTexture = null;
            Destroy(carInstanse);
            return _texture;
        }

        public void ReleaseTexture() {
            RenderTexture.ReleaseTemporary(_texture);
        }

        public Vector3 GetRenderCamera() {
            return _renderCamera.transform.position;
        }
    }
}