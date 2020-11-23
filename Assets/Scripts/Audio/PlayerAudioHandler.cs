using Events;
using UnityEngine;

namespace Audio {

    public class PlayerAudioHandler : MonoBehaviour {

        [SerializeField]
        private AudioSourcePlayer _audioDodgePlayer;

        [SerializeField]
        private AudioSourcePlayer _audioCrashPlayer;

        [SerializeField]
        private EventListener _playerDodgeCar;

        [SerializeField]
        private EventListener _playerCrashCar;

        private void Awake() {
            _playerDodgeCar.OnEventHappened += OnEventDodge;
            _playerCrashCar.OnEventHappened += OnEventCrash;
        }

        private void OnDestroy() {
            _audioDodgePlayer.Stop();
            _audioCrashPlayer.Stop();
            _playerDodgeCar.OnEventHappened -= OnEventDodge;
            _playerCrashCar.OnEventHappened -= OnEventCrash;
        }

        private void OnEventDodge() {
            _audioDodgePlayer.Play();
        }

        private void OnEventCrash() {
            _audioCrashPlayer.Play();
        }
    }
}
