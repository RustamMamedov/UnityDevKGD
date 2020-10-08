using System;
<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Events
{
    public class EventListener : MonoBehaviour {
=======
using UnityEngine;

namespace Events {

    public class EventListener : MonoBehaviour {

>>>>>>> master
        [SerializeField]
        private ScriptableEvent _someEvent;

        public event Action OnEventHappened = delegate { };

<<<<<<< HEAD
        private void OnEnable(){
            _someEvent.AddListener(EventHappened);
        }

        private void OnDisable(){
            _someEvent.RemoveListener(EventHappened);
        }

        private void EventHappened(){
            OnEventHappened.Invoke();
        }


    }
}

=======
        private void OnEnable() {
            _someEvent.AddListener(EventHappened);
        }

        private void OnDisable() {
            _someEvent.RemoveListener(EventHappened);
        }

        private void EventHappened() {
            OnEventHappened.Invoke();
        }
    }
}
>>>>>>> master
