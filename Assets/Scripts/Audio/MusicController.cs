using UnityEngine;
using System.Collections; 

namespace Audio {

    public class MusicController : MonoBehaviour {

        [SerializeField]
        private AudioSourcePlayer _menuMusicPlayer;

        [SerializeField]
        private AudioSourcePlayer _gameplayMusicPlayer;

        [SerializeField]
        private float _requiredTime;

        public void PlayMenuMusic() {
            StartCoroutine(PlayMenuMusicCoroutine());
        }

        public void PlayGameplayMusic() {
            StartCoroutine(PlayGameplayMusicCoroutine());
        }

        IEnumerator PlayMenuMusicCoroutine() {

            yield return _gameplayMusicPlayer.StopGradually(_requiredTime);
            _menuMusicPlayer.PlayMusic(_requiredTime);
        }

        IEnumerator PlayGameplayMusicCoroutine() {
            yield return _menuMusicPlayer.StopGradually(_requiredTime);
            _gameplayMusicPlayer.PlayMusic(_requiredTime);
        }
    }

}