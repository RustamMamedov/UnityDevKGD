using UnityEngine;
using System.Collections.Generic;

namespace Audio {

    public class MusicManager : MonoBehaviour {

        [SerializeField]
        private AudioSoursePlayer _menuMusicPlayer;

        [SerializeField]
        private AudioSoursePlayer _gameMusicPlayer;

        [SerializeField]
        private ScriptableFloatValue _volume;

        [SerializeField]
        private float _transitionTime;

        private void Awake() {
            _volume.value = 0.5f;
        }

        public void PlayMenuMusic() {
            _menuMusicPlayer.Play();
            StartCoroutine(_menuMusicPlayer.VolumeCorotuine(0f, _volume.value*0.5f, _transitionTime));
            StartCoroutine(_gameMusicPlayer.VolumeCorotuine(_volume.value * 0.5f, 0f, _transitionTime));
            
        }

        public void PlayGameMusic() {
            _gameMusicPlayer.Play();
            StartCoroutine(_menuMusicPlayer.VolumeCorotuine(_volume.value * 0.5f, 0f, _transitionTime));
            StartCoroutine(_gameMusicPlayer.VolumeCorotuine(0f, _volume.value * 0.5f, _transitionTime));
            
        }

        

        

        
    }
}