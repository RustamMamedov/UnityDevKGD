using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {

    public class RenderManager : MonoBehaviour {

        public static RenderManager instance;

        [SerializeField]
        private Camera _renderCamera;

        [SerializeField]
        private Transform _carRootTransform;

        private RenderTexture _texture;

        private void Awake() {
            if(instance != null) {
                Destroy(gameObject);
                instance = this;
            }
            instance = this;
        }

        public RenderTexture Render(GameObject prefab) {
            var carInstance = Instantiate(prefab, _carRootTransform);
            var _texture = RenderTexture.GetTemporary(64, 64, 16);
            _texture.antiAliasing = 8;
            _texture.Create();
            _renderCamera.targetTexture = _texture;
            _renderCamera.Render();
            _renderCamera.targetTexture = null;
            Destroy(carInstance);

            return _texture;
        }

        public void ReleaseTextures() {
            RenderTexture.ReleaseTemporary(_texture);
        }
    }
}

