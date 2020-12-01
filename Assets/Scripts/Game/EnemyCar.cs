
using UnityEngine;
using Events;
using Audio;

namespace Game {
    public class EnemyCar : Car {

        [SerializeField]
        private EventDispatcher _carCollisionEventDispatcher;

        [SerializeField]
        private ScriptableIntValue _currentScore;

        [SerializeField]
        private ScriptableIntValue _dodgedEnemyCarsNumber;

        [SerializeField]
        private ScriptableFloatValue _playerPositionZ;

        [SerializeField]
        private AudioSourcePlayer _dodgeSoundPlayer;

        private bool _dodged = false;

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                _carCollisionEventDispatcher.Dispatch();
            }
        }

        protected override void OnDisable() {
            _dodged = false;
            base.OnDisable();
        }



        protected override void UpdateBehaviour() {
            if (_playerPositionZ.value >= gameObject.transform.position.z && !_dodged) {
                _dodged = true;
                _currentScore.value += _carSettings.dodgeScore;
                _dodgedEnemyCarsNumber.value++;
                _dodgeSoundPlayer.Play();
            }
            base.UpdateBehaviour();
        }

    }
}

