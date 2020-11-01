using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Game;

namespace UI {
    public class InputController : MonoBehaviour {
        [SerializeField]
        private EventListeners _updateEventListener;

        [SerializeField]
        private EventDispatcher _touchEventDiptacher;

        [SerializeField]
        private ScriptableIntValue _touchSide;

        private void OnEnable() {
            _updateEventListener.OnEventHappened +=UpdateBehavour;
        }

        private void OnDisable() {
            _updateEventListener.OnEventHappened -= UpdateBehavour;
        }
        private void UpdateBehavour() {
            var touchPosition = Vector2.zero;
#if UNITY_EDITOR
            if (!Input.GetMouseButton(0)) { //если не ножата левая кнопка то выходим иначе
                return;
            }
            touchPosition = Input.mousePosition;//запоминаем позицию нажатия

#else
            if (Input.touchCount < 1) {
                return;
            }
            touchPosition = Input.touches[0].position;
#endif
            _touchSide.Value = (touchPosition.x > Screen.width * .5 ? 1 : -1);
            _touchEventDiptacher.Dispatch();
            _touchSide.Value = 0;
        }
    }
}