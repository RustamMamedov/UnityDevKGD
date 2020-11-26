using System.Collections.Generic;
using Events;
using Game;
using Sirenix.OdinInspector;
using UI;
using UnityEngine;

namespace Audio {

    public class AudioSourcePlayer : MonoBehaviour {

        [SerializeField]
        private EventListener _settingsChangedEventListener;

        [SerializeField]
        private bool _multipleSounds;

        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private SettingsScreen _settingsScreen;

        [ShowIf("_multipleSounds")]
        [SerializeField]
        private List<AudioClip> _backgroundMusic;

        private void Start() {
            _audioSource.volume = Save.Settings.volumeValue;
        }

        private void OnEnable() {
            _settingsChangedEventListener.OnEventHappened += SetVolume;
        }

        private void OnDisable() {
            _settingsChangedEventListener.OnEventHappened -= SetVolume;
            _audioSource.volume = Save.Settings.volumeValue;
        }

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

        private void SetVolume() {
            if (_settingsScreen != null) {
                _audioSource.volume = _settingsScreen.Volume;
            }
        }
    }
}
