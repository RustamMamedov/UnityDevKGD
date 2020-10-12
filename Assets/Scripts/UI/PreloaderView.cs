using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using Events;
using UnityEngine.UI;

namespace UI {
    public class PreloaderView : MonoBehaviour {

        [SerializeField]
        private ScriptableFoatValue _sceneLoadingValue;


        [SerializeField]
        private EventListener _updateListener;

        [SerializeField]
        private Image _prorgressImage;

        private void Awake() {
            _updateListener.OnEventHappened += UpdateBehavior;
        }
        
        private void nDestroy() {
            _updateListener.OnEventHappened-= UpdateBehavior;
        }
        private void UpdateBehavior() {
            //    
            _prorgressImage.fillAmount = _sceneLoadingValue.value;
        }

    }
}