using UnityEngine;
using System.Collections;

namespace Audio {

    public class MusicManager : MonoBehaviour {

        [SerializeField]
        private AudioSourcePlayer _menuMusicPlayer;

        [SerializeField]
        private AudioSourcePlayer _gameMusicPlayer;

        public void PlayMenuMusic() {
            _menuMusicPlayer.Play();
        }

        public void PlayGameMusic() {
            _gameMusicPlayer.Play();
        }

        public IEnumerator MuteMenuCoroutine() {
            while (_menuMusicPlayer.GetComponentInParent<AudioSource>().volume > 0) {
                _menuMusicPlayer.GetComponentInParent<AudioSource>().volume -= 0.1f;
                yield return new WaitForSeconds(0.1f);
            }
        }
        public IEnumerator MuteGameCoroutine() {
            while (_gameMusicPlayer.GetComponentInParent<AudioSource>().volume > 0) {
                _gameMusicPlayer.GetComponentInParent<AudioSource>().volume -= 0.001f;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}