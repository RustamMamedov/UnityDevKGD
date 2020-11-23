using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Audio {

    public class MusicManager : MonoBehaviour {

        // Fields.

        [SerializeField]
        private AudioSourcePlayer _menuMusicPlayer;


        // Methods.

        public void PlayMenuMusic() {
            _menuMusicPlayer.Play();
        }


    }

}