using System.Collections;
using UnityEngine;
using Game;

namespace Audio {

    public class MusicManager : MonoBehaviour {

        [SerializeField]
        private AudioSourcePlayer _menuMusicPlayer;

        [SerializeField]
        private AudioSourcePlayer _gameMusicPlayer;

        [SerializeField]
        private float _audioTime;

        [SerializeField]
        [Range(0f, 1f)]
        private float _minVolume;

        [SerializeField]
        [Range(0f,1f)]
        private float _maxVolume;

        [SerializeField]
        private ScriptableFloatValue _volume;

        public void PlayMenuMusic() {
            //_menuMusicPlayer.Play();
            StartCoroutine(PlaySound(_menuMusicPlayer));
        }
        public void StopMenuMusic() {
            //_menuMusicPlayer.Stop();
            StartCoroutine(StopSound(_menuMusicPlayer));
        }

        public void PlayGameMusic() {
            //_gameMusicPlayer.Play();
            StartCoroutine(PlaySound(_gameMusicPlayer));
        }
        public void StopGameMusic() {
            //_gameMusicPlayer.Stop();
            StartCoroutine(StopSound(_gameMusicPlayer));
        }
        private IEnumerator PlaySound(AudioSourcePlayer audio) {
            audio.Play();
            yield return StartCoroutine(VolumeSoundCoroutine(audio ,_minVolume, _maxVolume));
        }
        private IEnumerator StopSound(AudioSourcePlayer audio) {
            yield return StartCoroutine(VolumeSoundCoroutine(audio, _maxVolume, _minVolume));
            audio.Stop();
        }
        private IEnumerator VolumeSoundCoroutine(AudioSourcePlayer audio, float fromAlpha, float targetAlpha) {
            var timer = 0f;

            while (timer < _audioTime) {
                timer += Time.deltaTime;
                var audiosource = audio.GetComponent<AudioSource>();
                audiosource.volume = Mathf.Lerp(audiosource.volume, targetAlpha, timer / _audioTime);
                yield return null;
            }
        }
    }
}