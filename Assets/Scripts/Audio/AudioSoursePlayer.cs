using UnityEngine;
using Sirenix.OdinInspector;

namespace Audio {

    public class AudioSoursePlayer : MonoBehaviour {

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

    }
}