/*<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Events
{
    public class EventDispatcher : MonoBehaviour
    {
        [SerializeField]
        private ScriptableEvent _someEvent;

        private void Dispatch()
        {
            _someEvent.Dispatch();
        }
    }

}
=======*/
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Events {

    public class EventDispatcher : MonoBehaviour {

        [SerializeField]
        private ScriptableEvent _someEvent;

        public void Dispatch() {
            _someEvent.Dispatch();
        }
    }
}
//>>>>>>> master
