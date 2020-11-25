using UnityEngine;

namespace Audio {

    public class MusicManager : MonoBehaviour {

        [SerializeField]
        private AudioSoursePlayer _menuMusicPlayer;

        public void PlayMenuMusic() {
            _menuMusicPlayer.Play();
        }
    }
}