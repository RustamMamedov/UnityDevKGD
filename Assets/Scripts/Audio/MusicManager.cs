using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Audio {

    public class MusicManager : MonoBehaviour {

        [SerializeField]
        private AudioSourcePlayer _menuMusicPlayer;

        [SerializeField]
        private AudioSourcePlayer _gameplayMusicPlayer;

        [SerializeField]
        private float _fadeTime;

        [SerializeField]
        private float _maxMusicVolume;

        private string _currentScene;

        public void SetVolume(float value) {
            _maxMusicVolume = value;
            _menuMusicPlayer.GetComponent<AudioSource>().volume = value;
        }

        public void FadeIn(string sceneName) {
            _currentScene = sceneName;
            if (_currentScene == "Menu") {
                StartCoroutine(FadeInCoroutine(_menuMusicPlayer));
            } else if (_currentScene == "Gameplay") {
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

        private IEnumerator FadeInCoroutine(AudioSourcePlayer audioSourcePlayer) {
            audioSourcePlayer.Play();
            yield return StartCoroutine(FadeCoroutine(audioSourcePlayer.GetComponent<AudioSource>(), _maxMusicVolume));
        }

        private IEnumerator FadeOutCoroutine(AudioSourcePlayer audioSourcePlayer) {
            yield return StartCoroutine(FadeCoroutine(audioSourcePlayer.GetComponent<AudioSource>(), 0f));
            audioSourcePlayer.Stop();
        }

        private IEnumerator FadeCoroutine(AudioSource audioSource, float targetVolume) {
            var timer = 0f;
            var start = audioSource.volume;

            while (timer < _fadeTime) {
                timer += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(start, targetVolume, timer / _fadeTime);
                yield return null;
            }
            yield break;
        }   
    }
}
