using UnityEngine;
using Events;
using Audio;

namespace Game {

    public class EnemyCar : Car {

        [SerializeField] private EventDispatcher _carCollisionEventDispatcher;

        [SerializeField] private ScriptableIntValue _currentScore;

        [SerializeField] private CarSettings _dodgeScore;

        [SerializeField]
        private AudioSourcePlayer _audioSoursePlayer;

        public CarSettings DodgeScore => _dodgeScore;
        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                _carCollisionEventDispatcher.Dispatch();
                _currentScore.value = 0;
                _audioSoursePlayer.Play();  
                Debug.Log("CarCollision");
              

            }

        }
    }

}