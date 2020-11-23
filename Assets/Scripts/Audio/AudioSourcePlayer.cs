﻿using System.Collections; 
using Sirenix.OdinInspector;
using UnityEngine;

namespace Audio {

    public class AudioSourcePlayer : MonoBehaviour {

        [SerializeField]
        private AudioSource _audioSource;

        [Button]
        public void Play() {
            _audioSource.Play();
        }

        [Button]
        public void Stop() {
            _audioSource.Stop();
        }

        public void PlayMusic(float requiredTime) {
            _audioSource.volume = 0;
            Play();
            StartCoroutine(CoroutineMusicVolume(0f, 1f, requiredTime));
        }

        public IEnumerator StopGradually(float requiredTime) {
            yield return StartCoroutine(CoroutineMusicVolume(1f, 0f, requiredTime));
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