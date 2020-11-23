using UnityEngine;
using Events;

namespace Audio {

    public class MusicManager : MonoBehaviour {

        [SerializeField]
        private AudioSourcePlayer _menuMusicPlayer;

        [SerializeField]
        private AudioSourcePlayer _gameMusicPlayer;

        [SerializeField]
        private AudioSourcePlayer _dodged;

        [SerializeField]
        private AudioSourcePlayer _carCollision;

        [SerializeField]
        private EventListener _carDodgedEventListener;

        [SerializeField]
        private EventListener _carCollisionEventListener;

        private void OnEnable() {
            _carDodgedEventListener.OnEventHappened += OnCarDodged;
            _carCollisionEventListener.OnEventHappened += OnCarCollision;
        }

        private void OnCarDodged() {
            _dodged.Play();
        }

        private void OnCarCollision() {
            _carCollision.Play();
        }

        public void PlayMenuMusic() {
            PauseMusic();
            _menuMusicPlayer.Play();
        }
        public void PlayGameMusic() {
            PauseMusic();
            _gameMusicPlayer.Play();

        }

        private void PauseMusic() {
            _gameMusicPlayer.Stop();
            _menuMusicPlayer.Stop();
        }
    }
}