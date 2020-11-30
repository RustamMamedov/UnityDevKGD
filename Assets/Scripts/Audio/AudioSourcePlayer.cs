﻿using Sirenix.OdinInspector;
using UnityEngine;
using System.Collections;
using Game;

namespace Audio {

    public class AudioSourcePlayer : MonoBehaviour {

        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private ScriptableFloatValue _volume;

        [Button]
        public void Play() {
            _audioSource.volume = _volume.value;
            _audioSource.Play();
        }

        [Button]
        public void Stop() {
            _audioSource.Stop();
        }

        public IEnumerator AudioCoroutine(float fromVolume, float targetVolume, float timePause) {
            var timer = 0f;
            _audioSource.volume = fromVolume;

            while (timer < timePause) {
                timer += Time.deltaTime;
                _audioSource.volume = Mathf.Lerp(_audioSource.volume, targetVolume, timer / timePause);
                yield return null;
            }
            
            if (targetVolume == 0) {
                Stop();
            }
        }
    }
}