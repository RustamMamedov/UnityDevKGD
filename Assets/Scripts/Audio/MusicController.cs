using UnityEngine;

namespace Audio {

    public class MusicController : MonoBehaviour {

        [SerializeField]
        private AudioSourcePlayer _menuMusicPlayer;

        public void PlayMenuMusic() {
            _menuMusicPlayer.Play();
        }

    }

}