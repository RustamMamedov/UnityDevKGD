using Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class Car : MonoBehaviour {

        // Fields.

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private EventListener _carCollisionListener;

        [SerializeField]
        private CarSettings _carSettings;

        private float _currentSpeed;


        // Life cycle.

        private void Awake() {
            SubscribeToEvents();
        }


        // Event handling.

        private void SubscribeToEvents() {
            _updateEventListener.OnEventHappened += UpdateBehaviour;
            _carCollisionListener.OnEventHappened += OnCarCollision;
        }

        private void UnsubscribeFromEvents() {
            _updateEventListener.OnEventHappened -= UpdateBehaviour;
            _carCollisionListener.OnEventHappened -= OnCarCollision;
        }

        private void UpdateBehaviour() {
            Move();
        }

        private void OnCarCollision() {
            //UnsubscribeFromEvents();
            Debug.Log("CarCollision");
        }


        // Supportive methods.

        private void Move() {
            _currentSpeed += _carSettings.acceleration * Time.deltaTime;
            _currentSpeed = Mathf.Min(_currentSpeed, _carSettings.maxSpeed);
            transform.Translate(transform.forward * _currentSpeed * Time.deltaTime, Space.World);
        }


    }

}