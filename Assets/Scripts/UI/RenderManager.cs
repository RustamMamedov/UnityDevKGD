using UnityEngine;

namespace UI {


    public class RenderManager : MonoBehaviour {

        public static RenderManager Instance;

        [SerializeField]
        private Camera _renderCamera;

        [SerializeField]
        private Transform _rootTransform;

        private RenderTexture _texture;

        private void Awake() {
            Instance = this;
        }

        public RenderTexture Render(GameObject prefab) {
            var carInstance = Instantiate(prefab, _rootTransform);
            _texture = RenderTexture.GetTemporary(64, 64, 16);
            _texture.antiAliasing = 8;
            _texture.Create();
            _renderCamera.targetTexture = _texture;
            _renderCamera.Render();
            _renderCamera.targetTexture = null; 
            Destroy(carInstance);
            return _texture;
        }

        public void ReleaseTexture() {
            RenderTexture.ReleaseTemporary(_texture);
        }
    }
}