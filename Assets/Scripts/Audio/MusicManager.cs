using UnityEngine;
using Utilities;
using Values;

namespace Audio {

    public class MusicManager : SceneSingletonBase<MusicManager> {

        // Fields.

        [SerializeField]
        private AudioSourcePlayer _menuMusicPlayer;

        [SerializeField]
        private AudioSourcePlayer _gameMusicPlayer;

        [SerializeField]
        private ScriptableFloatValue _volumeChangeDurationValue;

        private AudioSourcePlayer _currentlyPlaying = null;


        // Methods.

        public void StopMusic(bool immediately = true) {
            if (_currentlyPlaying != null) {
                if (immediately) {
                    _currentlyPlaying.Stop();
                } else {
                    _currentlyPlaying.Stop(_volumeChangeDurationValue.value);
                }
                _currentlyPlaying = null;
            }
        }

        public void PlayMenuMusic(bool immediately = true) {
            StopMusic();
            if (immediately) {
                _menuMusicPlayer.Play();
            } else {
                _menuMusicPlayer.Play(_volumeChangeDurationValue.value);
            }
            _currentlyPlaying = _menuMusicPlayer;
        }

        public void PlayGameMusic(bool immediately = true) {
            StopMusic();
            if (immediately) {
                _gameMusicPlayer.Play();
            } else {
                _gameMusicPlayer.Play(_volumeChangeDurationValue.value);
            }
            _currentlyPlaying = _gameMusicPlayer;
        }


    }

}