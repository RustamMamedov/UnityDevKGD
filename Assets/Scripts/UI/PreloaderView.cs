using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using Events;
using UnityEngine.UI;
namespace UI {
    public class PreloaderView : MonoBehaviour {
        [SerializeField]
        private ScriptableFloatValue _SceneLoaderValue;

        [SerializeField]
        private EventListeners _updateList;

        [SerializeField]
        private Image _ProgressImage;

        private void Awake() {
            _updateList.OnEventHappened += UpdateBehavour;
        }

        private void OnDestroy() {
            _updateList.OnEventHappened -= UpdateBehavour;
        }

        private void UpdateBehavour() {
            _ProgressImage.fillAmount = _SceneLoaderValue.Value;
        }
    }
}
