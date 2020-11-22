using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio {

    public class MusicManager : MonoBehaviour {

        [SerializeField]
        private AudioSourcePlayer _menuAudioSourecePlayer;

        public void OnMenuPlayMusic() {
            _menuAudioSourecePlayer.Play();
        }

        public void OnStopedAllMusic() {
            _menuAudioSourecePlayer.Stop();
        }
    }
}
