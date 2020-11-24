using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio {
    public class MusicManager : MonoBehaviour {

        [SerializeField]
        private AudioSourcePlayer _menuMusicPlayer;

        [SerializeField]
        private AudioSourcePlayer _gameplayMusicPlayer;

        [SerializeField]
        public float time;

        public void PlayMenuMusic() {
            StartCoroutine(MenuMusicCoroutine());
        }

        public void PlayGameplayMusic() {
            StartCoroutine(GameplayMusicCoroutine());
        }

        private IEnumerator MenuMusicCoroutine() {
            yield return _gameplayMusicPlayer.StopMusic(time);
            _menuMusicPlayer.PlayMusic(time);
        }
        private IEnumerator GameplayMusicCoroutine() {
            yield return _menuMusicPlayer.StopMusic(time);
            _gameplayMusicPlayer.PlayMusic(time);
        }

    }
}
