using UnityEngine;

namespace Audio {

    public class MusicManager : MonoBehaviour {

        [SerializeField]
        private AudioSourcePlayer _menuMusicPlayer;

        [SerializeField]
        private AudioSourcePlayer _gameMusicPlayer;

        public void PlayMenuMusic() {
            _menuMusicPlayer.Play();
            StartCoroutine(_gameMusicPlayer.AudioCoroutine(0.14f, 0f, 2f));
            StartCoroutine(_menuMusicPlayer.AudioCoroutine(0f, 0.14f, 2f));
        }

        public void PlayGameMusic() {
            _gameMusicPlayer.Play();
            StartCoroutine(_gameMusicPlayer.AudioCoroutine(0f, 0.14f, 2f));
            StartCoroutine(_menuMusicPlayer.AudioCoroutine(0.14f, 0f, 2f));
        }
    }
}