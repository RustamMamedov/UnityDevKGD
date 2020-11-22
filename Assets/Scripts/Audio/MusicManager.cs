using UnityEngine;

namespace Audio {
    
    public class MusicManager : MonoBehaviour {
        
        [SerializeField]
        private AudioSourcePlayer _menuMusicPlayer;
        
        [SerializeField]
        private AudioSourcePlayer _gameMusicPlayer;

        public void PlayMenuMusic() {
            _gameMusicPlayer.FadeStop();
            _menuMusicPlayer.FadePlay();
        }

        public void PlayGameMusic() {
            _menuMusicPlayer.FadeStop();
            _gameMusicPlayer.FadePlay();
        }

    }
}


