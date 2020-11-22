using UnityEngine;
using System.Collections.Generic;

namespace Audio {

    public class MusicManager : MonoBehaviour {

        [SerializeField]
        private AudioSoursePlayer _menuMusicPlayer;
        [SerializeField]
        private AudioSoursePlayer _gameMusicPlayer;

        [SerializeField]
        [Range(0f, 1f)]
        private float _maxVolume;

        [SerializeField]
        private float _transitionTime;
        public void PlayMenuMusic() {
            _menuMusicPlayer.Play();
            StartCoroutine(_menuMusicPlayer.VolumeCorotuine(0f, _maxVolume, _transitionTime));
            StartCoroutine(_gameMusicPlayer.VolumeCorotuine(_maxVolume, 0f, _transitionTime));
            
        }

        public void PlayGameMusic() {
            _gameMusicPlayer.Play();
            StartCoroutine(_menuMusicPlayer.VolumeCorotuine(_maxVolume, 0f, _transitionTime));
            StartCoroutine(_gameMusicPlayer.VolumeCorotuine(0f, _maxVolume, _transitionTime));
            
        }

        

        

        
    }
}