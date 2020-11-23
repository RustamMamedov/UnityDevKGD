using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

namespace Audio {

    public class AudioSourcePlayer : MonoBehaviour {

        [SerializeField]
        private AudioSource _audioSource;

        [Range(0f, 1f)]
        [SerializeField]
        private float _maxVolume;

        [SerializeField]
        private float _fadeTime;

        [Button]
        public void Play() {
            _audioSource.Play();
        }

        [Button]
        public void Stop() {
            _audioSource.Stop();
        }

        public IEnumerator FadeIn() {
            yield return StartCoroutine(FadeCoroutine(_maxVolume));
        }

        public IEnumerator FadeOut() {
            yield return StartCoroutine(FadeCoroutine(0f));
        }

        private IEnumerator FadeCoroutine(float volumeTarget) {
            var time = 0f;
            var volumeStart = _audioSource.volume;
            while (time < _fadeTime) {
                time += Time.deltaTime;
                _audioSource.volume = Mathf.Lerp(volumeStart, volumeTarget, time / _fadeTime);
                yield return null;
            }
        }
    }
}