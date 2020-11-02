using System.Collections;
using System.Collections.Generic;
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
            var touchPosistion = Vector2.zero;
#if UNITY_EDITOR
            
            if (!Input.GetMouseButtonDown(0)) {
                return;
            }


            touchPosistion = Input.mousePosition;
#else


            if (Input.touchCount<1) {
                return;
            }
             touchPosistion = Input.touches[0].position;
           
#endif          

            _touchSide.value = touchPosistion.x > Screen.width * .5 ? 1 : -1;
       
            _touchEventDispatcher.Dispatch();
            _touchSide.value = 0;
        }
    }
}