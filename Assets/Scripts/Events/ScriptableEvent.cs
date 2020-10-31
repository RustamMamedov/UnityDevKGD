using System;
using System.Collections.Generic;
using UnityEngine;

namespace Events {

    [CreateAssetMenu(fileName = "Event", menuName = "Events/Event")]
    public class ScriptableEvent : ScriptableObject {

        private List<Action> _listeners;

        public void AddListener(Action action) {
            if (_listeners == null) {
                _listeners = new List<Action>();
            }

            if (!_listeners.Contains(action)) {
                _listeners.Add(action);
            }

        }

        public void RemoveListener(Action action) {
            if (_listeners == null) {
                return;
            }

            if (_listeners.Contains(action)) {
                _listeners.Remove(action);
            }

        }

        public void Dispatch() {
            if (_listeners == null) {
                return;
            }

            // It should be more effective just to go through listeners using integer index,
            // but I wanna try this, more dogmatic though costly approach.
            foreach (var listener in _listeners.ToArray()) {
                listener.Invoke();
            }

        }


    }

}
