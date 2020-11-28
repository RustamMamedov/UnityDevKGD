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
        public AudioSource audioSource;

        [SerializeField]
        private SettingsScreen _settingsScreen;

        [ShowIf("_multipleSounds")]
        [SerializeField]
        private List<AudioClip> _backgroundMusic;

        private void Start() {
            audioSource.volume = Save.Settings.volumeValue;
        }

        private void OnEnable() {
            _settingsChangedEventListener.OnEventHappened += SetVolume;
        }

        private void OnDisable() {
            _settingsChangedEventListener.OnEventHappened -= SetVolume;
        }

        [Button]
        public void Play() {
            audioSource.Play();
        }

        [Button]
        public void PlayRandom() {
            audioSource.clip = GetRandomClip();
            audioSource.Play();
        }

        [Button]
        public void Stop() {
            audioSource.Stop();
        }

        private AudioClip GetRandomClip() {
            return _backgroundMusic[Random.Range(0, _backgroundMusic.Count)];
        }

        private void SetVolume() {
            if (_settingsScreen != null) {
                audioSource.volume = _settingsScreen.Volume;
            }
        }
    }
}
