using UnityEngine;
using Events;
using Game;

namespace UI {
    public class InputController : MonoBehaviour {

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private EventDispatcher _touchEventDispather;

        [SerializeField]
        private ScriptableIntValue _touchSide;
        
        private void OnEnable() {
            _updateEventListener.OnEventHappened += UpdateBehaviour;
        }

        private void OnDisable() {
            _updateEventListener.OnEventHappened -= UpdateBehaviour;
        }

        private void UpdateBehaviour() {
            var touchPosition = Vector3.zero;
#if UNITY_EDITOR
            if(!Input.GetMouseButtonDown(0)) {
                return;
            }
            touchPosition = Input.mousePosition;

#else

            if(Input.touchCount<1) {
                return;
            }
            touchPosition = Input.touches[0].position;
#endif
            _touchSide.value = touchPosition.x > Screen.width * .5 ? 1 : -1;
            _touchEventDispather.Dispatch();
            _touchSide.value = 0;
        }
    }

}
