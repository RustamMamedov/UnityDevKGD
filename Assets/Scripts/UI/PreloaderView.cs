using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using Events;
using UnityEngine.UI;


namespace UI {

    public class PreloaderView : MonoBehaviour {

        [SerializeField]
        private ScriptableFloatValue _sceneLoadingValue;

        [SerializeField]
        EventListener _updateListener;

        [SerializeField]
        private Image _progressImage;


        private void Awake() {
            _updateListener.OnEventHappened += UpdateBehaviour;
        }

        private void OnDestroy() {
            _updateListener.OnEventHappened -= UpdateBehaviour;
        }

        private void UpdateBehaviour() {
            _progressImage.fillAmount = _sceneLoadingValue.value;
        }


    }

}
