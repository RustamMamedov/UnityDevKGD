using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Events;
using Game;
namespace UI {
    public class PreloaderView : MonoBehaviour {
        [SerializeField]
        private Image _loadImage;
        [SerializeField]
        private EventListeners _updateList;
        [SerializeField]
        private ScriptableFloatValue _loadValue;
        private void Awake() {
            _updateList.OnEventHappened += UpdateBehavour;
        }
        private void OnDestroy() {
            _updateList.OnEventHappened -= UpdateBehavour;
        }
        private void UpdateBehavour() {
            _loadImage.fillAmount = _loadValue.value;
        }

    }
}