using System.Collections;
using Events;
using Game;
using UI;
using UnityEngine;

namespace Audio {

    public class MusicManager : MonoBehaviour {

        [SerializeField]
        private AudioSourcePlayer _menuMusicPlayer;

        [SerializeField]
        private float _fadeTime;

        [SerializeField]
        private EventListener _gameSavedEventListener;

        [SerializeField]
        private EventListener _settingsChangedEventListener;

        [SerializeField]
        private SettingsScreen _settingsScreen;

        private float maxVolume;

        private void OnEnable() {
            _settingsChangedEventListener.OnEventHappened += OnSettingsChanged;
            _gameSavedEventListener.OnEventHappened += OnGameSaved;
        }

        private void OnDisable() {
            _settingsChangedEventListener.OnEventHappened -= OnSettingsChanged;
            _gameSavedEventListener.OnEventHappened -= OnGameSaved;
        }

        public void PlayMusic() {
            if (_menuMusicPlayer.audioSource.volume == 0) {
                MusicStop();
            }
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
            PlayMusic();
            StartCoroutine(MusicFade(0f, maxVolume));
        }

        private void OnGameSaved() {
            maxVolume = _settingsScreen.Volume;
        }

        private void OnSettingsChanged() {
            maxVolume = _settingsScreen.Volume;
            if (!_menuMusicPlayer.audioSource.isPlaying) {
                PlayMusic();
            }
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
