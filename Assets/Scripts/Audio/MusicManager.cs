using System.Collections;
using UnityEngine;
using Events;

namespace Audio {

    public class MusicManager : MonoBehaviour {

        public static MusicManager Instance;

        [SerializeField]
        private AudioSourcePlayer _menuMusicPlayer;

        [SerializeField]
        private AudioSourcePlayer _gameplayMusicPlayer;

        [SerializeField]
        private AudioSourcePlayer _carDodgeSoundPlayer;

        [SerializeField]
        private AudioSourcePlayer _carCollisionSoundPlayer;

        [SerializeField]
        private EventListener _carDodgeEventListener;

        [SerializeField]
        private EventListener _carCollisionEventListener;

        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            _carDodgeEventListener.OnEventHappened += PlayCarDodgeSound;
            _carCollisionEventListener.OnEventHappened += PlayCarCollisionSound;

            _menuMusicPlayer.Play();
            _gameplayMusicPlayer.Play();
        }

        private void PlayCarDodgeSound() {
            _carDodgeSoundPlayer.Play();
        }

        private void PlayCarCollisionSound() {
            _carCollisionSoundPlayer.Play();
        }

        public void PlayMenuMusic() {
            StartCoroutine(MenuMusicCoroutine());
        }

        public void PlayGameplayMusic() {
            StartCoroutine(GameplayMusicCoroutine());
        }

        private IEnumerator MenuMusicCoroutine() {
            yield return StartCoroutine(_gameplayMusicPlayer.FadeOut());
            StartCoroutine(_menuMusicPlayer.FadeIn());
        }

        private IEnumerator GameplayMusicCoroutine() {
            yield return StartCoroutine(_menuMusicPlayer.FadeOut());
            StartCoroutine(_gameplayMusicPlayer.FadeIn());
        }
    }
}