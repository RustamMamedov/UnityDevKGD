using System.Collections;
using System.Collections.Generic;
using Events;
using Game;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio {

    public class MusicManager : MonoBehaviour {

        [SerializeField]
		private AudioSource _menuMusicPlayer;
		
		[SerializeField]
		private AudioSource _gameMusicPlayer;

		[SerializeField] 
		private AudioMixer _audioMixer;
		
		[SerializeField] 
		private ScriptableFloatValue _musicVolumeScriptableFloatValue;
		
		[SerializeField] 
		private ScriptableFloatValue _gameVolumeScriptableFloatValue;

		[SerializeField] 
		private float _musicFadeTime;
		
		[SerializeField]
		private EventListener _playMenuMusicEventListener;
		
		[SerializeField]
		private EventListener _playGameMusicEventListener;

		private void OnEnable() {
			SubscribeToEvents();
			PlayMenuMusic();
		}

		private void Start() {
			SetAudioMixersVolume();
		}

		private void OnDisable() {
			UnsubscribeFromEvents();
		}
		
		private void SubscribeToEvents() {
			_playMenuMusicEventListener.OnEventHappened += PlayMenuMusic;
			_playGameMusicEventListener.OnEventHappened += PlayGameMusic;
		}
		
		private void UnsubscribeFromEvents() {
			_playMenuMusicEventListener.OnEventHappened -= PlayMenuMusic;
			_playGameMusicEventListener.OnEventHappened -= PlayGameMusic;
		}
		
		public void PlayMenuMusic() {
			if (_gameMusicPlayer.isPlaying) {
				StopGameMusic();
			}
			StartCoroutine(ChangeVolumeMenuMusicCoroutine(true, _menuMusicPlayer, _menuMusicPlayer.volume, 1f));
		}

		public void PlayGameMusic() {
			if (_menuMusicPlayer.isPlaying) {
				StopMenuMusic();
			}
			StartCoroutine(ChangeVolumeMenuMusicCoroutine(true, _gameMusicPlayer, _gameMusicPlayer.volume, 1f));
		}
		
		private void SetAudioMixersVolume() {
			_audioMixer.SetFloat("MusicVolume", _musicVolumeScriptableFloatValue.value);
			_audioMixer.SetFloat("GameVolume", _gameVolumeScriptableFloatValue.value);
		}

		public void StopMenuMusic() {
			StartCoroutine(ChangeVolumeMenuMusicCoroutine(false, _menuMusicPlayer, _menuMusicPlayer.volume, 0f));
		}

		public void StopGameMusic() {
			StartCoroutine(ChangeVolumeMenuMusicCoroutine(false, _gameMusicPlayer, _gameMusicPlayer.volume, 0f));
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
			while (timer < _musicFadeTime && musicPlayer.volume != desiredVolume) {
				timer += Time.deltaTime / _musicFadeTime; 
				musicPlayer.volume = Mathf.Lerp(musicPlayer.volume, desiredVolume, timer); 
				yield return null; 
			}
			if (stopMusic) {
				musicPlayer.Stop();
			}
		}
    }
}