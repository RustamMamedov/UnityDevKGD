using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace UI {

    public class RenderManager : SceneSingletonBase<RenderManager> {

        // Fields.
        
        [SerializeField]
        private Camera _renderCamera;

        [SerializeField]
        private GameObject _renderRoot;


        // Methods.

        public RenderTexture Render(GameObject prefab, int textureWidth, int textureHeight) {
            _renderRoot.SetActive(true);
            var instance = Instantiate(prefab, _renderRoot.transform);
            var texture = RenderTexture.GetTemporary(textureWidth, textureHeight, 16);
            texture.antiAliasing = 8;
            texture.Create();
            _renderCamera.targetTexture = texture;
            _renderCamera.Render();
            _renderCamera.targetTexture = null;
            DestroyImmediate(instance);
            _renderRoot.SetActive(false);
            return texture;
        }


    }

}
