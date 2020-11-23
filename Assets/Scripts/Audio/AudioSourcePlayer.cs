using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Audio {

    public class AudioSourcePlayer : MonoBehaviour {

        [SerializeField]
        private bool _multipleSounds;

        [SerializeField]
        private AudioSource _audioSource;

        [ShowIf("_multipleSounds")]
        [SerializeField]
        private List<AudioClip> _backgroundMusic;

        [Button]
        public void Play() {
            _audioSource.Play();
        }

        [Button]
        public void PlayRandom() {
            _audioSource.clip = GetRandomClip();
            _audioSource.Play();
        }

        [Button]
        public void Stop() {
            _audioSource.Stop();
        }

        private AudioClip GetRandomClip() {
            return _backgroundMusic[Random.Range(0, _backgroundMusic.Count)];
        }
    }
}
