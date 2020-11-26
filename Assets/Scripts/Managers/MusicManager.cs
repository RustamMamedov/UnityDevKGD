using System.Collections;
using Events;
using Game;
using UnityEngine;

namespace Audio {

    public class MusicManager : MonoBehaviour {

        [SerializeField]
        private AudioSourcePlayer _menuMusicPlayer;

        [SerializeField]
        private float _fadeTime;

        [SerializeField]
        private EventListener _gameSavedEventListener;

        private float maxVolume;

        private void OnEnable() {
            _gameSavedEventListener.OnEventHappened += OnGameSaved;
        }

        private void OnDisable() {
            _gameSavedEventListener.OnEventHappened -= OnGameSaved;
        }

        public void PlayMenuMusic() {
            _menuMusicPlayer.PlayRandom();
        }

        public void MusicStop() {
            _menuMusicPlayer.Stop();
        }

        public void MusicFadeIn() {
            StartCoroutine(MusicFade(maxVolume, 0f));
        }

        public void MusicFadeOut() {
            maxVolume = Save.Settings.volumeValue;
            PlayMenuMusic();
            StartCoroutine(MusicFade(0f, maxVolume));
        }

        private void OnGameSaved() {
            maxVolume = Save.Settings.volumeValue;
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
