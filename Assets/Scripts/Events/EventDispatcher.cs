using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Events {

    public class EventDispatcher : MonoBehaviour {

        [SerializeField]
        private ScriptableEvent _dispatchedEvent;

        public void Dispatch() {
            _dispatchedEvent.Dispatch();
        }


    }

}

