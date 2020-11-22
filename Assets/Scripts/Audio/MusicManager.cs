using UnityEngine;

namespace Audio {

    public class MusicManager : MonoBehaviour {

        [SerializeField]
        private AudioSourcePlayer _menuMusicPlayer;

        public void PlayrMenuMusic() {
            _menuMusicPlayer.Play();
        }
    }
}