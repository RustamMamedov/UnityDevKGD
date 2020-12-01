using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio {

    public class MusicManager : MonoBehaviour {

        [SerializeField]
        private AudioSourcePlayer _menuMusicPlayer;

        [SerializeField]
        private AudioSourcePlayer _gameMusicPlayer;
        public void PlayMenuMusic() {
            _menuMusicPlayer.Play();
            StartCoroutine(LoudMenuCoroutine());
        }
        public void PlayGameMusic() {
            _gameMusicPlayer.Play();
            StartCoroutine(LoudGameCoroutine());
        }
        private IEnumerator LoudGameCoroutine() {
            while (_gameMusicPlayer.GetComponentInParent<AudioSource>().volume < 1){
                _gameMusicPlayer.GetComponentInParent<AudioSource>().volume += 0.1f;
                yield return new WaitForSeconds(0.1f);
            }
        }
        private IEnumerator LoudMenuCoroutine() {
            while (_menuMusicPlayer.GetComponentInParent<AudioSource>().volume < 1) {
                _menuMusicPlayer.GetComponentInParent<AudioSource>().volume += 0.1f;
                yield return new WaitForSeconds(0.1f);
            }
        }
        public IEnumerator MuteMenuCoroutine() {
            while (_menuMusicPlayer.GetComponentInParent<AudioSource>().volume > 0) {
                _menuMusicPlayer.GetComponentInParent<AudioSource>().volume -= 0.1f;
                yield return new WaitForSeconds(0.1f);
            }
            _menuMusicPlayer.Stop();
        }
        public IEnumerator MuteGameCoroutine() {
            while (_gameMusicPlayer.GetComponentInParent<AudioSource>().volume > 0) {
                _gameMusicPlayer.GetComponentInParent<AudioSource>().volume -= 0.1f;
                yield return new WaitForSeconds(0.1f);
            }
            _gameMusicPlayer.Stop();
        }
    }
}
