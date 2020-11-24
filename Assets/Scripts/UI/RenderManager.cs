using UnityEngine;
using UnityEngine.UI;

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
            var carInctance = Instantiate(prefab,_rootTransform );
            var texture = RenderTexture.GetTemporary(64,64,16);
            texture.antiAliasing = 8;
            texture.Create();
            _renderCamera.targetTexture = texture;
            _renderCamera.Render();
            _renderCamera.targetTexture = null;
            Destroy(carInctance);
            return texture;
        }

        public void ReleaseTextures() {

            RenderTexture.ReleaseTemporary(_texture);
        }
    }
}