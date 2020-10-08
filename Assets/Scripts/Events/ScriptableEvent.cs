using System;
using System.Collections.Generic;
using UnityEngine;

namespace Events {
<<<<<<< HEAD
    [CreateAssetMenu(fileName = "Event", menuName = "Event")]
    public class ScriptableEvent : ScriptableObject{
=======

    [CreateAssetMenu(fileName = "Event", menuName = "Event")]
    public class ScriptableEvent : ScriptableObject {

>>>>>>> master
        private List<Action> _listeners;

        public void AddListener(Action action) {
            if (_listeners == null) {
                _listeners = new List<Action>();
            }

            if (_listeners.IndexOf(action) == -1) {
                _listeners.Add(action);
            }
        }

        public void RemoveListener(Action action) {
            if (_listeners == null || _listeners.IndexOf(action) == -1) {
                return;
            }

            _listeners.Remove(action);
        }

        public void Dispatch() {
<<<<<<< HEAD
            if (_listeners == null ){
                return;
            }

=======
            if (_listeners == null) {
                return;
            }
>>>>>>> master
            for (int i = 0; i < _listeners.Count; i++) {
                _listeners[i]();
            }
        }
    }
}