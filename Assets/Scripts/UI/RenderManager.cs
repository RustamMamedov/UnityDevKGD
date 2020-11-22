using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {

    public class RenderManager : MonoBehaviour {

        public static RenderManager Instance;

        [SerializeField]
        private Camera _cameraRender;

        [SerializeField]
        private Transform _carRootTransform;

        private RenderTexture _texture;

        private void Awake() {
            Instance = this;
        }

        public RenderTexture Render(GameObject prefab) {
            var carInstance = Instantiate(prefab,_carRootTransform);
            _texture = RenderTexture.GetTemporary(64, 64, 16);
            _texture.antiAliasing = 8;
            _texture.Create();
            _cameraRender.targetTexture = _texture;
            _cameraRender.Render();
            _cameraRender.targetTexture = null;
            Destroy(carInstance);

            return _texture;
        }

        public void ReleaseTexture() {
            RenderTexture.ReleaseTemporary(_texture);
        }
    }
}
