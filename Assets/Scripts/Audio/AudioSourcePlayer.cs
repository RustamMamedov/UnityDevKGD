using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;

namespace Audio {

    public class AudioSourcePlayer : MonoBehaviour {

        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private Slider _volumeSlider;

        [Button]
        public void Play() {
            _audioSource.Play();
        }

        [Button]
        public void Stop() {
            _audioSource.Stop();
        }

        public void PlayMusic(float time) {
            Play();
            StartCoroutine(MusicVolumeCoroutine(0.001f, PlayerPrefs.GetFloat("Volume"), time));
        }
        public IEnumerator StopMusic(float time) {
            yield return StartCoroutine(MusicVolumeCoroutine(_audioSource.volume, 0f, time));
            Stop();
        }

        private IEnumerator MusicVolumeCoroutine(float startVol, float finalVol, float time) {
            var timer = 0f;
            while (timer < time) {
                timer += Time.deltaTime;
                _audioSource.volume = Mathf.Lerp(startVol, finalVol, timer / time);
                yield return null;
            }
        }
    }
}
