using Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Values;

namespace UI {

    public class PreloaderView : MonoBehaviour {

        // Fields.

        [SerializeField]
        private ScriptableFloatValue _sceneLoadingProgress;

        [SerializeField]
        private EventListener _updateListener;

        [SerializeField]
        private Image _progressImage;


        // Life cycle.

        private void Awake() {
            _updateListener.OnEventHappened += UpdateBehaviour;
        }

        private void OnDestroy() {
            _updateListener.OnEventHappened -= UpdateBehaviour;
        }


        // Event handlers.

        private void UpdateBehaviour() {
            _progressImage.fillAmount = _sceneLoadingProgress.value;
        }


    }

}
