using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio {

    public class MusicManager : MonoBehaviour {

        [SerializeField]
		private AudioSource _menuMusicPlayer;
		
		[SerializeField]
		private AudioSource _gameMusicPlayer;

		[SerializeField] 
		private float _musicFadeTime;
		
		public void PlayMenuMusic() {
			StartCoroutine(ChangeVolumeMenuMusicCoroutine(true, _menuMusicPlayer, 0f, 1f));
		}

		public void PlayGameMusic() {
			StartCoroutine(ChangeVolumeMenuMusicCoroutine(true, _gameMusicPlayer, 0f, 1f));
		}
		
		public void StopMenuMusic() {
			StartCoroutine(ChangeVolumeMenuMusicCoroutine(false, _menuMusicPlayer, 1f, 0f));
		}

		public void StopGameMusic() {
			StartCoroutine(ChangeVolumeMenuMusicCoroutine(false, _gameMusicPlayer, 1f, 0f));
		}

		private IEnumerator ChangeVolumeMenuMusicCoroutine(bool playMusic, AudioSource musicPlayer,float startVolume, float desiredVolume) {
			if (musicPlayer.volume == desiredVolume) {
				yield break;
			}
			var timer = 0f;
			var stopMusic = !playMusic;
			musicPlayer.volume = startVolume;
			if (playMusic) {
				musicPlayer.Play();
			}
			while (timer < _musicFadeTime) { 
				timer += Time.deltaTime; 
				musicPlayer.volume = Mathf.Lerp(musicPlayer.volume, desiredVolume, timer / _musicFadeTime); 
				yield return null; 
			}
			if (stopMusic) {
				musicPlayer.Stop();
			}
		}
    }
}