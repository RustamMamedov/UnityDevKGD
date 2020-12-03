using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

namespace Audio {

    public class AudioSourcePlayer : MonoBehaviour {

        [SerializeField]
        private AudioSource _audioSource;

        public bool IsPlaying => _audioSource.isPlaying;

        [Button]
        public void Play() {
            _audioSource.Play();
        }

        [Button]
        public void Stop() {
            _audioSource.Stop();
        }

        public void SetVolume(float value) {
            _audioSource.volume = value;
        }
    }
}