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

        [SerializeField]
        private float startVolume;
        [Button]
        public void Play() {
            _audioSource.Play();
            Debug.Log("play");
        }

        [Button]
        public void Stop() {
            _audioSource.Stop();
        }

        private void Awake() {
            _volumeChangeEvent.OnEventHappened += OnGlobalVolumeChanged;
            startVolume = _audioSource.volume;
            if(Settings.Instance!=null) {
                _audioSource.volume = startVolume * Settings.Instance.GlobalVolume;
            }
        }
        
        void OnGlobalVolumeChanged() {
            _audioSource.volume = startVolume * Settings.Instance.GlobalVolume;
        }

        public void SetAudioSourceVolume(float volume) {
            _audioSource.volume = volume;
            startVolume = volume;
        }
        public float GetAudioSourceVolume() {
            return _audioSource.volume;
        }
    }
}