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

        [SerializeField]
        [PropertyRange(16, 256)]
        private int _textureSize;


        // Methods.

        public RenderTexture Render(GameObject prefab) {
            _renderRoot.SetActive(true);
            var instance = Instantiate(prefab, _renderRoot.transform);
            var texture = RenderTexture.GetTemporary(_textureSize, _textureSize, 16);
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
