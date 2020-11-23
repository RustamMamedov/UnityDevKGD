using UnityEngine;
using Events;

namespace Audio {

    public class MusicManager : MonoBehaviour {

        [SerializeField]
        private AudioSourcePlayer _menuMusicPlayer;

        [SerializeField]
        private AudioSourcePlayer _carDodgeSoundPlayer;

        [SerializeField]
        private AudioSourcePlayer _carCollisionSoundPlayer;

        [SerializeField]
        private EventListener _carDodgeEventListener;

        [SerializeField]
        private EventListener _carCollisionEventListener;

        private void Awake() {
            _carDodgeEventListener.OnEventHappened += PlayCarDodgeSound;
            _carCollisionEventListener.OnEventHappened += PlayCarCollisionSound;
        }

        private void PlayCarDodgeSound() {
            _carDodgeSoundPlayer.Play();
        }

        private void PlayCarCollisionSound() {
            _carCollisionSoundPlayer.Play();
        }

        public void PlayrMenuMusic() {
            _menuMusicPlayer.Play();
        }
    }
}