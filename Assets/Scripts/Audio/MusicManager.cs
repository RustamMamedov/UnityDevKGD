using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio {

    public class MusicManager : MonoBehaviour {

        [SerializeField]
        private AudioSourcePlayer _menuMusicPlayer;

        public void PlayMenuMusic() {
            _menuMusicPlayer.Play();
        }

        public void StopMenuMusic() {
            _menuMusicPlayer.Stop();
        }
    }
}