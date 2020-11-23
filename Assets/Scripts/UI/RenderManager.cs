using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {

    public class RenderManager : MonoBehaviour {

        public static RenderManager Instance { get; private set; }

        [SerializeField]
        private Camera _renderCamera;

        [SerializeField]
        private Transform _rootTransform;

        [SerializeField]
        private Light _dirLight;

        private RenderTexture _texture;

        private Vector3 _renderCameraPosition;

        private Quaternion _renderCameraRotation;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
        }

        public RenderTexture Render(GameObject prefab) {
            _dirLight.enabled = true;
            var carInstance = Instantiate(prefab, _rootTransform);

            _texture = RenderTexture.GetTemporary(64, 64, 16);
            _texture.antiAliasing = 8;
            _texture.Create();

            _renderCamera.transform.position = _renderCameraPosition;
            _renderCamera.transform.rotation = _renderCameraRotation;
            _renderCamera.targetTexture = _texture;
            _renderCamera.Render();
            _renderCamera.targetTexture = null;

            Destroy(carInstance);
            _dirLight.enabled = false;
            return _texture;

        }

        public void ReleaseTextures() {
            RenderTexture.ReleaseTemporary(_texture);
        }

        public void SetCameraTransform(Vector3 position, Quaternion rotation) {
            _renderCameraPosition = position;
            _renderCameraRotation = rotation;
        }
    }
}
