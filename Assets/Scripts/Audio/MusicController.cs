using UnityEngine;
using System.Collections;

namespace Audio {

    public class MusicController : MonoBehaviour {

        [SerializeField]
        private AudioSourcePlayer _menuMusicPlayer;

        [SerializeField]
        private AudioSourcePlayer _gameplayMusicPlayer;

        [SerializeField]
        private float _fadeTime;

        [SerializeField]
        private float _maxMusicVolume;

        private string _currentScene;

        public void FadeIn(string sceneName) {
            _currentScene = sceneName;
            if (_currentScene == "Menu") {
                _menuMusicPlayer.Play();
                StartCoroutine(FadeInCoroutine(_menuMusicPlayer));
            } else if (_currentScene == "Gameplay") {
                _gameplayMusicPlayer.Play();
                StartCoroutine(FadeInCoroutine(_gameplayMusicPlayer));
            }
        }

        public void FadeOut() {
            if (_currentScene == "Menu") {
                StartCoroutine(FadeOutCoroutine(_menuMusicPlayer));
            } else if (_currentScene == "Gameplay") {
                StartCoroutine(FadeOutCoroutine(_gameplayMusicPlayer));
            }
        }

        private IEnumerator FadeInCoroutine(AudioSourcePlayer audioSource) {
            yield return StartCoroutine(FadeCoroutine(audioSource, _maxMusicVolume));
        }

        private IEnumerator FadeOutCoroutine(AudioSourcePlayer audioSource) {
            yield return StartCoroutine(FadeCoroutine(audioSource, 0f));
        }

        private IEnumerator FadeCoroutine(AudioSourcePlayer audioSource, float targetVolume) {
            var timer = 0f;
            var start = audioSource.GetComponent<AudioSource>().volume;

            while (timer < _fadeTime) {
                timer += Time.deltaTime;
                audioSource.GetComponent<AudioSource>().volume = Mathf.Lerp(start, targetVolume, timer / _fadeTime);
                yield return null;
            }
            yield break;
        }   
    }
}
