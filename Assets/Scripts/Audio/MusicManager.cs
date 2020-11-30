using UnityEngine;
using Game;

namespace Audio {

    public class MusicManager : MonoBehaviour {

        [SerializeField]
        private AudioSourcePlayer _menuMusicPlayer;

        [SerializeField]
        private AudioSourcePlayer _gameMusicPlayer;

        [SerializeField]
        private ScriptableFloatValue _volume;

        public void PlayMenuMusic() {
            _menuMusicPlayer.Play();
            StartCoroutine(_gameMusicPlayer.AudioCoroutine(_volume.value, 0f, 2f));
            StartCoroutine(_menuMusicPlayer.AudioCoroutine(0f, _volume.value, 2f));
        }

        public void PlayGameMusic() {
            _gameMusicPlayer.Play();
            StartCoroutine(_gameMusicPlayer.AudioCoroutine(0f, _volume.value, 2f));
            StartCoroutine(_menuMusicPlayer.AudioCoroutine(_volume.value, 0f, 2f));
        }
    }
}