using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Audio {
    
    public class AudioSourcePlayer : MonoBehaviour {

        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField] 
        private float _fadeInTime = 3f;
        
        [SerializeField] 
        private float _fadeOutTime = 1f;

        private Coroutine _fadeInCoroutine;
        private Coroutine _fadeOutCoroutine;
        
        [Button]
        public void Play() {
            _audioSource.volume = 1f;
            _audioSource.Play();
        }
        
        [Button]
        public void Stop() {
            _audioSource.Stop();
        }

        [Button]
        public void FadeStop() {
            if (_fadeInCoroutine != null) {
                StopCoroutine(_fadeInCoroutine);
                _fadeInCoroutine = null;
            }
                
            _fadeOutCoroutine = StartCoroutine(FadeOutCoroutine(_fadeOutTime));
        }
        
        [Button]
        public void FadePlay() {
            if (_fadeOutCoroutine != null) {
                StopCoroutine(_fadeOutCoroutine);
                _fadeOutCoroutine = null;
            }
            
            _fadeInCoroutine = StartCoroutine(FadeInCoroutine(_fadeInTime));
        }

        private IEnumerator FadeOutCoroutine(float duration) {
            var currentTime = 0f;
            var startVolume = _audioSource.volume;

            while (currentTime < duration) {
                currentTime += Time.deltaTime;
                _audioSource.volume = Mathf.Lerp(startVolume, 0f, currentTime / duration);
                yield return null;
            }
            
            _audioSource.Stop();
        }
        
        private IEnumerator FadeInCoroutine(float duration) {
            var currentTime = 0f;
            var startVolume = _audioSource.volume = 0f;
            
            _audioSource.Play();

            while (currentTime < duration) {
                currentTime += Time.deltaTime;
                _audioSource.volume = Mathf.Lerp(startVolume, 1f, currentTime / duration);
                yield return null;
            }
        }
    }
}

