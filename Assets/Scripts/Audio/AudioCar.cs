using Events;
using UnityEngine;

namespace Audio {

    public class AudioCar : MonoBehaviour {

        [SerializeField]
        private AudioSourcePlayer _dodgeSound;

        [SerializeField]
        private AudioSourcePlayer _collisionSound;

        [SerializeField]
        private EventListener _carDodgedEvent;

        [SerializeField]
        private EventListener _carCollisionEvent;

        private void OnEnable() {
            _carCollisionEvent.OnEventHappened += OnCarCollision;
            _carDodgedEvent.OnEventHappened += OnCarDodged;
        }

        private void OnDisable() {
            _carCollisionEvent.OnEventHappened -= OnCarCollision;
            _carDodgedEvent.OnEventHappened -= OnCarDodged;
        }

        private void OnCarDodged() {
            _dodgeSound.Play();
        }

        private void OnCarCollision() {
            _collisionSound.Play();
        }
    }
}
