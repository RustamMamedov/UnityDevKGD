using System;
using UnityEngine;

namespace UI {
    
    public class RenderManager : MonoBehaviour {
    
        public static RenderManager Instance { get; private set; }

        [SerializeField]
        private Camera _renderCamera;

        private RenderTexture _texture;

        [SerializeField] 
        private Transform _carRootTransform;

        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
        }

        public RenderTexture Render(GameObject prefab, Vector3 cameraPosition, Quaternion cameraRotation) {
            var carInstance = Instantiate(prefab, _carRootTransform);
            
            _texture = RenderTexture.GetTemporary(64, 64, 16);
            _texture.antiAliasing = 8;
            _texture.Create();

            _renderCamera.targetTexture = _texture;
            _renderCamera.transform.position = cameraPosition;
            _renderCamera.transform.rotation = cameraRotation;
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

