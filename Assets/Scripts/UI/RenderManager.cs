using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

namespace UI {

    public class RenderManager : MonoBehaviour {

        public static RenderManager Instance;

        [SerializeField]
        private Camera _cameraRender;

        [SerializeField]
        private Light _light;

        [SerializeField]
        private Transform _carRootTransform;

        private RenderTexture _texture;

        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        public RenderTexture Render(CarSettings carSettings) {
            _light.enabled = true;
            if (_cameraRender == null) Debug.Log("NULL");
            var carInstance = Instantiate(carSettings.renderCarPrefab, _carRootTransform);
            _texture = RenderTexture.GetTemporary(64, 64, 16);
            _texture.antiAliasing = 8;
            _texture.Create();
            _cameraRender.transform.position = carSettings.positionCamera+transform.position;
            _cameraRender.transform.rotation = Quaternion.Euler(carSettings.rotationCamera);
            _cameraRender.targetTexture = _texture;
            _cameraRender.Render();
            _light.enabled = false;
            _cameraRender.targetTexture = null;
            Destroy(carInstance);
            return _texture;
        }

        public void ReleaseTexture() {
            RenderTexture.ReleaseTemporary(_texture);
        }
    }
}
