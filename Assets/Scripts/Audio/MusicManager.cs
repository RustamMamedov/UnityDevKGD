using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio {

    public class MusicManager : MonoBehaviour {

        [SerializeField]
        private AudioSoursePlayer _menuMusicPlayer;

        [SerializeField]
        private AudioSoursePlayer _gameMusicPlayer;


        private AudioSource menuPlayer => _menuMusicPlayer._audioSourse;
        private AudioSource gamePlayer => _gameMusicPlayer._audioSourse;

        public void PlayMenuMusic() {
            _menuMusicPlayer.Play();
            RiseMusicVolume(menuPlayer);
        }

        public void StopMenuMusic() {
            LowMusicVolume(menuPlayer);
            if (menuPlayer.volume == 0f) {
                _menuMusicPlayer.Stop();
            }
        }

        public void PlayGameMusic() {
            _gameMusicPlayer.Play();
            RiseMusicVolume(gamePlayer);
        }

        public void StopGameMusic() {
            LowMusicVolume(gamePlayer);
            if (gamePlayer.volume == 0f) {
                _menuMusicPlayer.Stop();
            }
        }

        private void LowMusicVolume(AudioSource player) {
            StartCoroutine(MusicLowCoroutine(player));

        }

        private void RiseMusicVolume(AudioSource player) {
            StartCoroutine(MusicRiseCoroutine(player));

        }

        private IEnumerator MusicLowCoroutine(AudioSource player) {
            yield return StartCoroutine(ChangeMusicVolumeCoroutine(player, 0.07f, 0f));
        }

        private IEnumerator MusicRiseCoroutine(AudioSource player) {
            yield return StartCoroutine(ChangeMusicVolumeCoroutine(player, 0f, 0.07f));
        }

        private IEnumerator ChangeMusicVolumeCoroutine(AudioSource player, float fromVolume, float toVolume) {
            var timer = 0f;
            player.volume = fromVolume;

            while (timer < 3f) {
                timer += Time.deltaTime;
                player.volume = Mathf.Lerp(player.volume, toVolume, timer / 3f);
                yield return null;
            }
        }
    }
}
