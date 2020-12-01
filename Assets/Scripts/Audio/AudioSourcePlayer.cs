using Sirenix.OdinInspector;
using UnityEngine;
using UI;
using Events;

namespace Audio {

    public class AudioSourcePlayer : MonoBehaviour {

        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private EventListener _volumeChangeEvent;

        private float _startVolume;

        private float _changedVolume;

        [Button]
        public void Play() {
            _audioSource.Play();
        }

        [Button]
        public void Stop() {
            _audioSource.Stop();
        }

        private void Awake() {
            _changedVolume = 1;
            _volumeChangeEvent.OnEventHappened += OnGlobalVolumeChanged;
            _startVolume = _audioSource.volume;
            if (Settings.Instance != null) {
                _audioSource.volume = _startVolume * Settings.Instance.GlobalVolume;
            }
        }

        void OnGlobalVolumeChanged() {
            if (_changedVolume == 0) {
                _audioSource.volume = 0;

            }
            else {
                _audioSource.volume = _startVolume * Settings.Instance.GlobalVolume;

            }
        }

        public void SetAudioSourceVolume(float volume) {
            _audioSource.volume = volume;
            _changedVolume = volume;
        }
        public float GetAudioSourceVolume() {
            return _audioSource.volume;
        }
    }
}