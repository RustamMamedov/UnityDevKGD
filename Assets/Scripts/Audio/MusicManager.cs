using UnityEngine;

namespace Audio {

    public class MusicManager : MonoBehaviour {

        [SerializeField]
        private AudioSourcePlayer _menuMusicPlayer;

        [SerializeField]
        private AudioSourcePlayer _gameMusicPlayer;

        public void PlayMenuMusic() {
            _menuMusicPlayer.Play();
        }
        public void StopMenuMusic() {
            _menuMusicPlayer.Stop();
        }

        public void PlayGameMusic() {
            _gameMusicPlayer.Play();
        }
        public void StopGameMusic() {
            _gameMusicPlayer.Stop();
        }
    }
}