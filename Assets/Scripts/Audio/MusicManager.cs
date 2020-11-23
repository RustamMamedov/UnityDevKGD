using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio {

    public class MusicManager : MonoBehaviour {

        [SerializeField]
        private AudioSourcePlayer _menuMusicPlayer;

        [SerializeField]
        private AudioSourcePlayer _gameplayMusic;

        [SerializeField]
        private AudioSourcePlayer _dodgeSound;

        [SerializeField]
        private AudioSourcePlayer _crachSound;

        public void PlayMenuMusic() {
            _gameplayMusic.Stop();
            _menuMusicPlayer.Play();
        }

        public void PlayGameplayMusic() {
           _menuMusicPlayer.Stop();
           _gameplayMusic.Play();
            
        }

        public void PlayDodgeSound() {
            _dodgeSound.Play();
        }

        public void PlayCrashSound() {
            _crachSound.Play();
        }
    }
}

