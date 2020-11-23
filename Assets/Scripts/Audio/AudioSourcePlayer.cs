using Sirenix.OdinInspector;
using UnityEngine;

namespace Audio {

    public class AudioSourcePlayer : MonoBehaviour {

        [SerializeField]
        private AudioSource _audioSource;

        [Button]
        public void Play() {
            _audioSource.Play();
            Debug.Log("play");
        }

        [Button]
        public void Stop() {
            _audioSource.Stop();
        }

        public void SetAudioSourceVolume(float volume) {
            _audioSource.volume = volume;
        }
        public float GetAudioSourceVolume() {
            return _audioSource.volume;
        }
    }
}