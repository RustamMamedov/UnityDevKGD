using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Events {
    
    public class EventListener : MonoBehaviour {

        [SerializeField]
        private ScriptableEvent _listenedEvent;

        public event Action OnEventHappened = delegate {};

        private void OnEnable() {
            _listenedEvent.AddListener(EventHandler);
        }

        private void OnDisable() {
            _listenedEvent.RemoveListener(EventHandler);
        }

        private void EventHandler() {
            OnEventHappened.Invoke();
        }


    }

}

