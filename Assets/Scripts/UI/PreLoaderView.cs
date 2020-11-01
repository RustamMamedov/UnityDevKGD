using Events;
using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    
    public class PreLoaderView : MonoBehaviour {

        [SerializeField]
        private ScriptableFloatValue _sceneLoadingValue;

        [SerializeField]
        private EventListener _updateListener;

        [SerializeField]
        private Image _progressImage;

        private void Awake() {
            _updateListener.OnEventHappened += UpdateBehavior;
        }

        private void OnDestroy() {
            _updateListener.OnEventHappened -= UpdateBehavior;
        }

        private void UpdateBehavior() {
            _progressImage.fillAmount = _sceneLoadingValue.value;
        }
    }
}