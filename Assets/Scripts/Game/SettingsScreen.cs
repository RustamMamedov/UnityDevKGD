using Events;
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UI;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Game {

	public class SettingsScreen : MonoBehaviour {
		
		[SerializeField] 
		private Button _cancelButton;

		[SerializeField] 
		private Button _applyButton;

		[SerializeField] 
		private AudioMixer _audioMixer;

		[SerializeField] 
		private AudioSource _clickClackAudioSource;

		[SerializeField] 
		private Slider _musicVolumeSlider;
		
		[SerializeField] 
		private Slider _gameVolumeSlider;

		[SerializeField] 
		private Toggle _isHardToggle;

		[SerializeField] 
		private Toggle _isNightToggle;
		
		[SerializeField] 
		private Toggle _isCrazyModeToggle;

		[SerializeField] 
		private ScriptableFloatValue _musicVolumeScriptableFloatValue;
		
		[SerializeField] 
		private ScriptableFloatValue _gameVolumeScriptableFloatValue;

		[SerializeField] 
		private ScriptableBoolValue _isNightScriptableBoolValue;
		
		[SerializeField] 
		private ScriptableBoolValue _isHardScriptableBoolValue;
		
		[SerializeField] 
		private ScriptableBoolValue _isCrazyModeScriptableBoolValue;
		
		[SerializeField] 
		private EventDispatcher _settingsGotSavedEventDispatcher;

		private float _gameVolume;
		private float _musicVolume;
		private bool _isNight;
		private bool _isHard;
		private bool _isCrazy;

		private void Awake() {
			_applyButton.onClick.AddListener(OnApplyButtonClick);
			_cancelButton.onClick.AddListener(OnCancelButtonClick);
		}

		private void OnEnable() {
			LoadFromScriptableObject();
			SetDataOnUIComponents();
		}
		
		private void OnCancelButtonClick() {
			SetDataOnUIComponents();
			ShowMenuScreen();
		}
		
		private void OnApplyButtonClick() {
			SaveToScriptableObject();
			_settingsGotSavedEventDispatcher.Dispatch();
			ShowMenuScreen();
		}

		public void SetMusicVolume(float volume) {
			_audioMixer.SetFloat("MusicVolume", volume);
		}
		
		public void SetGameVolume(float volume) {
			_audioMixer.SetFloat("GameVolume", volume);
		}

		public void SetDifficultyLevel(bool isHard) {
			_isHard = isHard;
			PlayClickClackSound(isHard);
		}
		
		public void SetDayCycle(bool isNight) {
			_isNight = isNight;
			PlayClickClackSound(isNight);
		}
		
		public void SetMode(bool isCrazy) {
			_isCrazy = isCrazy;
			PlayClickClackSound(isCrazy);
		}

		private void PlayClickClackSound(bool isOn) {
			if (isOn) {
				_clickClackAudioSource.pitch = 1f;
			}
			else {
				_clickClackAudioSource.pitch = 0.9f;
			}
			_clickClackAudioSource.Play();
		}
		
		private void SaveToScriptableObject() {
			_musicVolumeScriptableFloatValue.value = _musicVolumeSlider.value;
			_gameVolumeScriptableFloatValue.value = _gameVolumeSlider.value;
			_isHardScriptableBoolValue.value = _isHard;
			_isNightScriptableBoolValue.value = _isNight;
			_isCrazyModeScriptableBoolValue.value = _isCrazy;
		}

		private void LoadFromScriptableObject() {
			_musicVolume = _musicVolumeScriptableFloatValue.value;
			_gameVolume = _gameVolumeScriptableFloatValue.value;
			_isHard = _isHardScriptableBoolValue.value;
			_isNight = _isNightScriptableBoolValue.value;
			_isCrazy = _isCrazyModeScriptableBoolValue.value;
		}

		private void SetDataOnUIComponents() {
			SetMusicVolume(_musicVolume);
			_musicVolumeSlider.value = _musicVolume;
			SetGameVolume(_gameVolume);
			_gameVolumeSlider.value = _gameVolume;
			_isHardToggle.isOn = _isHardScriptableBoolValue.value;
			_isNightToggle.isOn = _isNightScriptableBoolValue.value;
			_isCrazyModeToggle.isOn = _isCrazyModeScriptableBoolValue.value;
		}

		private void ShowMenuScreen() {
			StartCoroutine(WaitAndShowScreen());
		}
		
		private IEnumerator WaitAndShowScreen() {
			yield return new WaitForSeconds(0.1f);
			UIManager.Instance.ShowMenuScreen();
		}
	}
}