using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

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

        public void PlayMusic(float growingTime) {
             StartCoroutine(GradualIncrease(growingTime));
        }

        public void StopMusic(float lowingTime) {
             StartCoroutine(GradualDecline(lowingTime));
        }

        private IEnumerator GradualIncrease(float necessaryTime) {
            Play();
            yield return StartCoroutine(MusicVolume(1f, necessaryTime));
        }

        private IEnumerator GradualDecline(float necessaryTime) {
            yield return StartCoroutine(MusicVolume(0f, necessaryTime));
            Stop();
        }

        private IEnumerator MusicVolume(float finalSound, float necessaryTime) {
            var time = 0f;

            while (time < necessaryTime) {
                time += Time.deltaTime;
                _audioSource.volume = Mathf.Lerp(_audioSource.volume, finalSound, time / necessaryTime);
                yield return null;
            }
        }
    }
}

