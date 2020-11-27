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

        [SerializeField]
        private float _neccesaryTime;

        public void PlayMenuMusic() {
            _gameplayMusic.StopMusic(_neccesaryTime);
            _menuMusicPlayer.PlayMusic(_neccesaryTime);
        }

        public void PlayGameplayMusic() {
            _menuMusicPlayer.StopMusic(_neccesaryTime);
            _gameplayMusic.PlayMusic(_neccesaryTime);
            
        }

        public void PlayDodgeSound() {
            _dodgeSound.Play();
        }

        public void PlayCrashSound() {
            _crachSound.Play();
        }
    }
}

