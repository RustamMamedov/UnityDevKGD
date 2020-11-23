using UnityEngine;
using System.Collections;

namespace Audio {

    public class MusicManager : MonoBehaviour {

        [SerializeField]
        private AudioSourcePlayer _menuMusicPlayer;

        [SerializeField]
        private AudioSourcePlayer _gameplayMusicPlayer;

        [SerializeField]
        private float _musicFadeTime;

        public void PlayMenuMusic() {
            _menuMusicPlayer.Play();
        }

        public void StopMenuMusic() {
            StartCoroutine(StopMusicCoroutine(_menuMusicPlayer, _menuMusicPlayer.GetAudioSourceVolume()));
        }


        public void StopGameplayMusic() {
            StartCoroutine(StopMusicCoroutine(_gameplayMusicPlayer, _gameplayMusicPlayer.GetAudioSourceVolume()));
        }

        private IEnumerator StopMusicCoroutine(AudioSourcePlayer audioPlayer, float startVolume) {
            float time = 0;
            while (audioPlayer.GetAudioSourceVolume() > 0) {
                time += Time.deltaTime;
                audioPlayer.SetAudioSourceVolume(Mathf.Lerp(startVolume, 0, time / _musicFadeTime));
                yield return null;
            }

            if (audioPlayer == _menuMusicPlayer) {
                StartCoroutine(StartMusicCoroutine(_gameplayMusicPlayer, startVolume));
            }
            else {
                StartCoroutine(StartMusicCoroutine(_menuMusicPlayer, startVolume));
            }
        }
        private IEnumerator StartMusicCoroutine(AudioSourcePlayer audioPlayer, float targetValue) {
            audioPlayer.SetAudioSourceVolume(0);
            audioPlayer.Play();
            float time = 0;
            while (audioPlayer.GetAudioSourceVolume() < targetValue) {
                Debug.Log("hello");
                time += Time.deltaTime;
                audioPlayer.SetAudioSourceVolume(Mathf.Lerp(0, targetValue, time / _musicFadeTime));
                yield return null;
            }
            audioPlayer.SetAudioSourceVolume(targetValue);
        }
    }
}