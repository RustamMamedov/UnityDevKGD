using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio {

    public class MusicManager : MonoBehaviour {

        [SerializeField]
        private AudioSourcePlayer _menuMusicPlayer;

        [SerializeField]
        private AudioSourcePlayer _gameMusicPlayer;

        public void PlayMenuMusic() {
            _menuMusicPlayer.Play(); 
            StartCoroutine(_menuMusicPlayer.Vol(0f, 1f, 1f));
            StartCoroutine(_gameMusicPlayer.Vol(1f, 0f, 1f));
        }

        public void PlayGameMusic() {
            _gameMusicPlayer.Play();
            StartCoroutine(_gameMusicPlayer.Vol(0f, 1f, 1f));
            StartCoroutine(_menuMusicPlayer.Vol(1f, 0f, 1f));
        }

    }
}
