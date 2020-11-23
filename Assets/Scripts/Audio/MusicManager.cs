using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Audio {

    public class MusicManager : SceneSingletonBase<MusicManager> {

        // Fields.

        [SerializeField]
        private AudioSourcePlayer _menuMusicPlayer;


        // Methods.

        public void PlayMenuMusic() {
            _menuMusicPlayer.Play();
        }


    }

}