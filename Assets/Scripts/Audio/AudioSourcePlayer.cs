using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using Game;
using Events;
using System;

namespace Audio {

    public class AudioSourcePlayer : MonoBehaviour {

        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private ScriptableFloatValue _currentVolume;

        [SerializeField]
        private EventListener _volumeSliderPositionChangedEventListener;

        [Button]
        public void Play() {
            _audioSource.volume = _currentVolume.value;
            _audioSource.Play();
        }

        [Button]
        public void Stop() {
            _audioSource.Stop();
        }


        private void Awake() {
            _audioSource.volume = PlayerPrefs.GetFloat("SavedVolume");
            _audioSource.maxDistance = PlayerPrefs.GetFloat("SavedVolume");
            _volumeSliderPositionChangedEventListener.OnEventHappened += OnVolumeSliderPositionChanged;
        }

        private void OnVolumeSliderPositionChanged() {
            _audioSource.volume = _currentVolume.value;
        }

        public void PlayMusic(float growingTime) {
             StartCoroutine(GradualIncrease(growingTime));
        }

        public void StopMusic(float lowingTime) {
             StartCoroutine(GradualDecline(lowingTime));
        }

        private IEnumerator GradualIncrease(float necessaryTime) {
            Play();
            yield return StartCoroutine(MusicVolume(_currentVolume.value, necessaryTime));
        }

        private IEnumerator GradualDecline(float necessaryTime) {
            yield return StartCoroutine(MusicVolume(0f, necessaryTime));
            Stop();
        }

        private IEnumerator MusicVolume(float finalSound, float necessaryTime) {
            var time = 0f;

            while (time < necessaryTime && _audioSource.volume != finalSound) {
                time += Time.deltaTime;
                _audioSource.volume = Mathf.Lerp(_audioSource.volume, finalSound, time / necessaryTime);
                yield return null;
            }
        }
    }
}

