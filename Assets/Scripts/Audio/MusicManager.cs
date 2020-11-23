using System.Collections;
using UnityEngine;

namespace Audio {

    public class MusicManager : MonoBehaviour {

        [SerializeField]
        private AudioSourcePlayer _menuMusicPlayer;

        // public void PlayMenuMusic() {
        //     _menuMusicPlayer.Play();
        // }

         [SerializeField] 
        private AudioSourcePlayer _gameplayMusicPlayer; 

        public AudioSourcePlayer gameplayMusicPlayer => _gameplayMusicPlayer;
 
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