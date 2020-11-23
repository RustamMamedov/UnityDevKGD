using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

namespace Audio {

    public class MusicManager : MonoBehaviour {

        [SerializeField]
        private AudioSourcePlayer _menuMusicPlayer;

        [SerializeField]
        private AudioSourcePlayer _gameplayMusicPlayer;

        [SerializeField]
        private float _requiredTime;

        private void Awake() {
            _gameplayMusicPlayer.Stop();
        }

        public void PlayMenuMusic() {
            _gameplayMusicPlayer.StopMusic(_requiredTime);
            _menuMusicPlayer.PlayMusic(_requiredTime);
        }

        public void PlayGameplayMusic() {
            _menuMusicPlayer.StopMusic(_requiredTime);
            _gameplayMusicPlayer.PlayMusic(_requiredTime);
        }
    }
}

