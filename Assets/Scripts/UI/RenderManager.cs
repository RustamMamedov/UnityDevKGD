using UnityEngine;
using System.Collections;

namespace UI {

    public class RenderManager : MonoBehaviour {

        [SerializeField]
        private Camera _renderCamera;

        [SerializeField]
        private Transform _rootTransform;

        [SerializeField]
        private Light _carSceneLight;

        private RenderTexture _texture;

        public static RenderManager Instance;

        private void Awake() {
           if (Instance != null) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public RenderTexture Render(GameObject prefab, Vector3 cameraPosition, Quaternion cameraRotation) {
            var carInstance = Instantiate(prefab, _rootTransform);
            var texture = RenderTexture.GetTemporary(64, 64, 16);
            _carSceneLight.enabled = true;

            texture.antiAliasing = 8;
            texture.Create();

            _renderCamera.targetTexture = texture;
            _renderCamera.transform.position = cameraPosition;
            _renderCamera.transform.rotation = cameraRotation;
            _renderCamera.Render();

            _renderCamera.targetTexture = null;
            Destroy(carInstance);
            _carSceneLight.enabled = false;

            return texture;
        }

        public void ReleaseTextures() {
            RenderTexture.ReleaseTemporary(_texture);
        }
    }
}