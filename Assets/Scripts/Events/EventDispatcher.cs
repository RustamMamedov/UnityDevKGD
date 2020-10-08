using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Events {
<<<<<<< HEAD
    public class EventDispatcher : MonoBehaviour {
=======

    public class EventDispatcher : MonoBehaviour {

>>>>>>> master
        [SerializeField]
        private ScriptableEvent _someEvent;

        public void Dispatch() {
            _someEvent.Dispatch();
        }
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> master
