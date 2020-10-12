<<<<<<< HEAD
﻿using Events;
using UnityEngine;

namespace Game
{
    public class Car : MonoBehaviour
    {
        [SerializeField]
        private EventListener _updateEventListener;

        private void Awake()
        {
            _updateEventListener.OnEventHappened += Move;
                  
        }

        private void Move()
        {
            Debug.Log("Move");
        }
    }
=======
﻿using Events;
using UnityEngine;

namespace Game {

    public class Car : MonoBehaviour {

        [SerializeField]
        private EventListener _updateEventListener;

        private void Awake() {
            _updateEventListener.OnEventHappened += Move;
        }

        private void Move() {
            Debug.Log("Move");
        }
    }
>>>>>>> master
}