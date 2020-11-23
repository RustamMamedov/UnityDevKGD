using System.Collections;
using UnityEngine;

namespace Audio {

    public class MusicManager : MonoBehaviour {

        [SerializeField]
        private AudioSourcePlayer _menuMusicPlayer;

        [SerializeField]
        private float _fadeTime;

        public void PlayMenuMusic() {
            _menuMusicPlayer.PlayRandom();
        }

        public void MusicStop() {
            _menuMusicPlayer.Stop();
        }

        public void MusicFadeIn() {
            StartCoroutine(MusicFade(0.1f, 0f));
        }

        public void MusicFadeOut() {
            PlayMenuMusic();
            StartCoroutine(MusicFade(0f, 0.1f));
        }

        private IEnumerator MusicFade(float volume, float desiredVolume) {
            float timer = 0f;
            _menuMusicPlayer.TryGetComponent<AudioSource>(out var audioSource);
            audioSource.volume = volume;

            while (timer <= _fadeTime) {
                timer += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(audioSource.volume, desiredVolume, timer / _fadeTime);
                yield return null;
            }
        }
    }
}
