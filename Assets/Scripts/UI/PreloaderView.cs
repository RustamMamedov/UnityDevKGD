using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using Events;
using UnityEngine.UI;

namespace UI {


    public class PreloaderView : MonoBehaviour {
        [SerializeField] private ScriptableFloatValue _sceneLoadingValue;

        [SerializeField] private EventListener _upadateListener;

        [SerializeField] private Image _progressImage;

        private void Awake() {
            _upadateListener.OnEventHappened += UpdateBehaviour;
        }

        private void OnDestroy() {
            _upadateListener.OnEventHappened -= UpdateBehaviour;
        }

        private void UpdateBehaviour() {
            _progressImage.fillAmount = _sceneLoadingValue.value;
        }
    }
}