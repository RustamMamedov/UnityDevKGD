using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace UI {

    public class RenderManager : SceneSingletonBase<RenderManager> {

        // Fields.
        
        [InfoBox("$" + nameof(GetCameraDistanceDescription), VisibleIf = nameof(CanShowCameraDistance))]
        [SerializeField]
        private Camera _renderCamera;

        private string GetCameraDistanceDescription() =>
            $"Distance between camera and render center is {_renderCamera.transform.position.magnitude} units.";

        private bool CanShowCameraDistance => _renderCamera != null;

        [SerializeField]
        private GameObject _renderRoot;

        private Vector3 _normalizedCameraPosition;


        // Methods.

        protected override void Awake() {
            base.Awake();
            _normalizedCameraPosition = _renderCamera.transform.position.normalized;
        }

        public RenderTexture Render(GameObject prefab, float cameraDistance, int textureWidth, int textureHeight) {
            _renderRoot.SetActive(true);
            var instance = Instantiate(prefab, _renderRoot.transform);
            var texture = RenderTexture.GetTemporary(textureWidth, textureHeight, 16);
            texture.antiAliasing = 8;
            texture.Create();
            _renderCamera.transform.position = _normalizedCameraPosition * cameraDistance;
            _renderCamera.targetTexture = texture;
            _renderCamera.Render();
            _renderCamera.targetTexture = null;
            DestroyImmediate(instance);
            _renderRoot.SetActive(false);
            return texture;
        }


    }

}
