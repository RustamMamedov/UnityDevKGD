using UnityEngine;
using Events;
using Game;
using Packages.Rider.Editor;

namespace UI {
    public class ImnputController : MonoBehaviour {
        [SerializeField]
        private EventListener _updateEventListener;
        [SerializeField]
        private EventDispatcher _touchEventDispatcher;
        [SerializeField]
        private ScriptableIntValue _touchSide;
        private void OnEnable() {
            _updateEventListener.OnEventHappened += UpdateBehaviour;
        }

        private void OnDisable() {
            _updateEventListener.OnEventHappened -= UpdateBehaviour;
        }

        private void UpdateBehaviour() {
            var touchPosition = Vector2.zero;
#if UNITY_EDITOR
            if (!Input.GetMouseButtonDown(0)) {
                return;
            }
            touchPosition = Input.mousePosition;
#else
            if (Input.touchCount < 1) {
                return;
            }
            touchPosition = Input.touches[0].position;
#endif



            _touchSide.value = touchPosition.x > Screen.width * .5f ? 1 : -1;
            _touchEventDispatcher.Dispatch();
            _touchSide.value = 0;
        }

    }
}