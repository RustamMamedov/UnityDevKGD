﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Events
{
    public class EventListener : MonoBehaviour
    {
        [SerializeField]
        private ScriptableEvent _someEvent;

        public event Action OnEventHappened = delegate { };

        private void OnEnable()
        {
            _someEvent.AddListener(EventHappened);
        }
        private void OnDiseble()
        {
            _someEvent.RemoveListener(EventHappened);
        }

        private void EventHappend()
        {
            OnEventHappened.Invoke();
        }


    }
}