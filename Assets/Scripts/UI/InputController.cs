using Events;
using UnityEngine;
using Values;

namespace UI {
    
    public class InputController : MonoBehaviour {

        // Fields.

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private EventDispatcher _touchEventDispatcher;

        [SerializeField]
        private ScriptableIntValue _touchSideValue;


        // Life cycle.

        private void OnEnable() {
            _updateEventListener.OnEventHappened += UpdateBehaviour;
        }

        private void OnDisable() {
            _updateEventListener.OnEventHappened -= UpdateBehaviour;
        }


        // Event handling.

        private void UpdateBehaviour() {

            var touchPosition = Vector2.zero;

#if UNITY_EDITOR
            if (!Input.GetMouseButton(0)) {
                return;
            }
            touchPosition = Input.mousePosition;
#else
            if (Input.touchCount < 1) {
                return;
            }
            touchPosition = Input.touches[0].position;
#endif

            _touchSideValue.value = touchPosition.x > Screen.width * 0.5f ? 1 : -1;
            _touchEventDispatcher.Dispatch();
            _touchSideValue.value = 0;

        }


    }

}
