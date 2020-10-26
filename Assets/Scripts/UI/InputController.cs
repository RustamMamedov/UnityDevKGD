using UnityEngine;
using Events;
using Game;
namespace UI {

    public class InputController : MonoBehaviour {

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

            if (!Input.GetMouseButton(0)) {
                return;
            }

            /*if (Input.touchCount < 1) {
                return;
            }*/

            //touchPosition = Input.touches[0].position;
            touchPosition = Input.mousePosition;

            _touchSide.value = touchPosition.x > Screen.width * 0.5 ? 1 : -1;
            Debug.Log($"_touchSideValue{_touchSide.value}");
            _touchEventDispatcher.Dispatch();
            _touchSide.value = 0;
        }
    }
}
