using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using Game;
using Events;

namespace Audio {

    public class AudioSourcePlayer : MonoBehaviour {

        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private ScriptableFloatValue _volumeSetting;

        [SerializeField]
        private EventListener _volumeChangeListener;

        private void OnEnable() {
            ChangeVolume();
            _volumeChangeListener.OnEventHappened += ChangeVolume;
        }

        private void OnDisable() {
            _volumeChangeListener.OnEventHappened -= ChangeVolume;
        }

        [Button]
        public void Play() {
            _audioSource.Play();
        }

        [Button]
        public void Stop() {
            _audioSource.Stop();
        }

        public void PlayMusic(float requiredTime) {
            StartCoroutine(PlayGradually(requiredTime));
        }

        public void StopMusic(float requiredTime) {
            StartCoroutine(StopGradually(requiredTime));
        }

        private void ChangeVolume() {
            _audioSource.volume = _volumeSetting.value;
        }

        IEnumerator PlayGradually(float requiredTime) {
            Play();
            yield return StartCoroutine(CoroutineMusicVolume(0f, _volumeSetting.value, requiredTime));
        }

        IEnumerator StopGradually(float requiredTime) {
            yield return StartCoroutine(CoroutineMusicVolume(_volumeSetting.value, 0f, requiredTime));
            Stop();
        }

        private IEnumerator CoroutineMusicVolume(float from, float to, float requiredTime) {
            var time = 0f;
            _audioSource.volume = from;

            while (time < requiredTime) {
                time += Time.deltaTime;
                _audioSource.volume = Mathf.Lerp(_audioSource.volume, to, time / requiredTime);
                yield return null;
            }
        }
    }
}

