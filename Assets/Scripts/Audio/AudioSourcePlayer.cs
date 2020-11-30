using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Game;


namespace Audio {
    public class AudioSourcePlayer : MonoBehaviour {

        [SerializeField]
        private AudioSource _audioSource;

        [Button]
        public void Play() {
            _audioSource.volume = _volume.value;
            _audioSource.Play();
        }

        [Button]
        public void Stop() {
            _audioSource.Stop();
        }

        [SerializeField]
        private ScriptableFloatValue _volume;

        public IEnumerator Vol(float from, float to, float time) {
            float timer = 0f;
            while (timer <= time) {
                _audioSource.volume = Mathf.Lerp(from, to, timer/time);
                timer += Time.deltaTime;
                yield return null;
            }

            if (to == 0f) {
                _audioSource.Stop();
            }
        }
    }
}
