using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class DodgeCountView : MonoBehaviour {

        // Fields.

        [SerializeField]
        private CarSettings _carSettings;

        [SerializeField]
        private RawImage _rawImage;

        private RenderTexture _renderTexture;


        // Life cycle.

        public void Start() {
            _renderTexture = RenderManager.Instance.Render(_carSettings.renderableCarPrefab);
            _rawImage.texture = _renderTexture;
        }

        private void OnDestroy() {
            _rawImage.texture = null;
            if (_renderTexture != null) {
                _renderTexture.Release();
            }
        }


    }

}
