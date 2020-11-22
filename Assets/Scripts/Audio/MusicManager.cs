using System.Collections;
using UnityEngine;

namespace Audio {

    public class MusicManager : MonoBehaviour {

        [SerializeField]
        private AudioSourcePlayer _menuMusicPlayer;

        [SerializeField]
        private AudioSourcePlayer _gameMusicPlayer;

        [SerializeField]
        private float _fadeTime = .3f;

        private AudioSourcePlayer _currentPlayer;

        public void PlayMenuMusic() {
            if (_menuMusicPlayer.IsPlaying) {
                return;
            }
            StartCoroutine(FadeMusicTo(_menuMusicPlayer));
        }

        public void PlayGameMusic() {
            if (_gameMusicPlayer.IsPlaying) {
                return;
            }
            StartCoroutine(FadeMusicTo(_gameMusicPlayer));
        }

        private IEnumerator FadeMusicTo(AudioSourcePlayer to) {
            if (_currentPlayer == null) {
                yield return StartCoroutine(FadeMusicIn(to));
                yield break;
            }

            yield return StartCoroutine(FadeMusicOut(_currentPlayer));
            StartCoroutine(FadeMusicIn(to));
        }

        private IEnumerator FadeMusicIn(AudioSourcePlayer to) {
            if (!to.IsPlaying) {
                to.Play();
            }
            if (_currentPlayer == to) {
                yield break;
            }
            _currentPlayer = to;

            var timer = 0f;
            var halfFadeTime = _fadeTime / 2f;
            while (timer < halfFadeTime) {
                timer += Time.deltaTime;
                var volume = Mathf.Lerp(0f, 1f, timer / halfFadeTime);
                to.SetVolume(volume);
                yield return null;
            }
        }

        private IEnumerator FadeMusicOut(AudioSourcePlayer to) {
            var timer = 0f;
            var halfFadeTime = _fadeTime / 2f;
            while (timer < halfFadeTime) {
                timer += Time.deltaTime;
                var volume = Mathf.Lerp(1f, 0f, timer / halfFadeTime);
                to.SetVolume(volume);
                yield return null;
            }
            to.Stop();
        }

    }
}