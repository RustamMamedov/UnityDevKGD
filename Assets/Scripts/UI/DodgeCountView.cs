using Events;
using Game;
using Sirenix.OdinInspector;
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
        private EventListener _updateEventListener;

        [SerializeField]
        private RawImage _icon;

        [SerializeField]
        [PropertyRange(4, 128)]
        private int _iconTextureSize;

        [SerializeField]
        private Text _text;

        private RenderTexture _renderTexture;


        // Life cycle.

        private void OnEnable() {
            _updateEventListener.OnEventHappened += UpdateBehaviour;
        }

        public void Start() {
            _renderTexture = RenderManager.Instance.Render(
                _carSettings.renderableCarPrefab, _carSettings.renderDistance, _iconTextureSize, _iconTextureSize);
            _icon.texture = _renderTexture;
        }

        private void UpdateBehaviour() {
            _text.text = _carSettings.dodgesCountValue.value.ToString();
        }

        private void OnDisable() {
            _updateEventListener.OnEventHappened += UpdateBehaviour;
        }

        private void OnDestroy() {
            _icon.texture = null;
            if (_renderTexture != null) {
                _renderTexture.Release();
            }
        }


    }

}
