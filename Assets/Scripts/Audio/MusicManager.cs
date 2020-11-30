using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

namespace Audio {

    public class MusicManager : MonoBehaviour {

        [SerializeField]
        private AudioSourcePlayer _menuMusicPlayer;

        [SerializeField]
        private AudioSourcePlayer _gameMusicPlayer;

        [SerializeField]
        private ScriptableFloatValue _volume;

        public void PlayMenuMusic() {
            _menuMusicPlayer.Play(); 
            StartCoroutine(_menuMusicPlayer.Vol(0f, _volume.value, 1f));
            StartCoroutine(_gameMusicPlayer.Vol(_volume.value, 0f, 1f));
        }

        public void PlayGameMusic() {
            _gameMusicPlayer.Play();
            StartCoroutine(_gameMusicPlayer.Vol(0f, _volume.value, 1f));
            StartCoroutine(_menuMusicPlayer.Vol(_volume.value, 0f, 1f));
        }

    }
}
