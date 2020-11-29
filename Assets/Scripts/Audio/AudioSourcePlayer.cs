using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Audio {

    public class AudioSourcePlayer : MonoBehaviour {

        [SerializeField]
        private AudioSource _audioSourece;

        [SerializeField]
        [Range(0f, 1f)]
        private float _minVolume = 0f;

        [SerializeField]
        [Range(0f, 1f)]
        private float _maxVolume = 1f;

        [Button]
        public void Play() {
            _audioSourece.Play();
        }

        [Button]
        public void Stop() {
            _audioSourece.Stop();
        }

        public IEnumerator PlayGradually(float timeAudio) {
            Play();
            yield return StartCoroutine(CoroutineMusicVolume(_minVolume, _maxVolume, timeAudio));
        }

        public IEnumerator StopGradually(float timeAudio) {
            yield return StartCoroutine(CoroutineMusicVolume(_maxVolume,_minVolume, timeAudio));
            Stop();
        }

        private IEnumerator CoroutineMusicVolume(float fromAlfa,float targetAlfa, float timeAudio) {
            var time = 0f;
            _audioSourece.volume = fromAlfa;

            while (time < timeAudio) {
                time += Time.deltaTime;
                _audioSourece.volume = Mathf.Lerp(_audioSourece.volume, targetAlfa, time/ timeAudio);
                yield return null;
            }
        }
    }
}
