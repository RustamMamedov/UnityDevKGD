﻿using UnityEngine;

namespace Events {

    public class EventDispatcher : MonoBehaviour {


        [SerializeField]
        private ScriptableEvent _someEvent;

        public void Dispatch() {
            _someEvent.Dispatch();
        }

    }
}

