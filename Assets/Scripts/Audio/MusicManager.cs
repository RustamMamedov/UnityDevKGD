using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Audio {

    public class MusicManager : SceneSingletonBase<MusicManager> {

        // Fields.

        [SerializeField]
        private AudioSourcePlayer _menuMusicPlayer;

        private AudioSourcePlayer _currentlyPlaying = null;


        // Methods.

        public void StopMusic() {
            if (_currentlyPlaying != null) {
                _currentlyPlaying.Stop();
                _currentlyPlaying = null;
            }
        }

        public void PlayMenuMusic() {
            StopMusic();
            _menuMusicPlayer.Play();
            _currentlyPlaying = _menuMusicPlayer;
        }


    }

}