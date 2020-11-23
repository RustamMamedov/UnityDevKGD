using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Audio {

    public class AudioSourcePlayer : MonoBehaviour {

        [SerializeField]
        private AudioSource _audioSourece;

        [Button]
        public void Play() {
            _audioSourece.Play();
        }


        public void PlayGradually(float timeAudio) {
            
            StartCoroutine(CoroutineMusicVolume(0f,1f, timeAudio));
        }

        [Button]
        public void Stop() {
            _audioSourece.Stop();
        }

        public void StopGradually(float timeAudio) {
            StartCoroutine(CoroutineMusicVolume(1f,0f, timeAudio));
        }

        private IEnumerator CoroutineMusicVolume(float fromAlfa,float targetAlfa, float timeAudio) {
            var time = 0f;
            _audioSourece.volume = fromAlfa;

            while (time < timeAudio) {
                time += Time.deltaTime;
                _audioSourece.volume = Mathf.Lerp(_audioSourece.volume, targetAlfa, time/ timeAudio);
                yield return null;
            }

            if (targetAlfa == 0) {
                Stop();
            }
        }
    }
}
