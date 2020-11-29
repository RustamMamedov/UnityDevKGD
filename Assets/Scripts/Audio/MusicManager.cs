using System;
using Events;
using Game;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio {
    
    public class MusicManager : MonoBehaviour {
        
        [SerializeField]
        private AudioSourcePlayer _menuMusicPlayer;
        
        [SerializeField]
        private AudioSourcePlayer _gameMusicPlayer;

        [SerializeField] 
        private EventListener _volumeChangedListener;
        
        [SerializeField] 
        private EventListener _settingsLoadedListener;

        [SerializeField] 
        private ScriptableFloatValue _volumeAsset;

        [SerializeField] 
        private AudioMixer _audioMixer;

        private void Awake() {
            _volumeChangedListener.OnEventHappened += OnVolumeChangedBehaviour;
            _settingsLoadedListener.OnEventHappened += OnSettingsLoaded;

            UpdateMixerVolume(0.0001f);
        }

        private void OnDestroy() {
            _volumeChangedListener.OnEventHappened -= OnVolumeChangedBehaviour;
            _settingsLoadedListener.OnEventHappened -= OnSettingsLoaded;
        }

        public void PlayMenuMusic() {
            _gameMusicPlayer.FadeStop();
            _menuMusicPlayer.FadePlay();
        }

        public void PlayGameMusic() {
            _menuMusicPlayer.FadeStop();
            _gameMusicPlayer.FadePlay();
        }

        private void UpdateMixerVolume(float multiplier) {
            _audioMixer.SetFloat("MasterVol",  Mathf.Log10(multiplier) * 20f);
        }

        private void OnVolumeChangedBehaviour() {
            UpdateMixerVolume(_volumeAsset.value);
        }

        private void OnSettingsLoaded() {
            UpdateMixerVolume(_volumeAsset.value);
        }

    }
}


