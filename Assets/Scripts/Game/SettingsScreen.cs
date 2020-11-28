using Events;
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
		private Toggle _isHardToggle;
		
		[SerializeField] 
		private Toggle _isNightToggle;

		[SerializeField] 
		private EventDispatcher _settingsGotSavedEventDispatcher;
		
		private const string playerPrefsName = "settings";

		private float _initialGameVolume;
		private float _initialMusicVolume;

		private void Awake() {
			_applyButton.onClick.AddListener(OnApplyButtonClick);
			_cancelButton.onClick.AddListener(OnCancelButtonClick);
		}

		private void OnEnable() {
			_audioMixer.GetFloat("MusicVolume", out _initialGameVolume);
			_audioMixer.GetFloat("GameVolume", out _initialMusicVolume);
		}
		
		private void OnCancelButtonClick() {
			SetMusicVolume(_initialMusicVolume);
			SetGameVolume(_initialGameVolume);
			ShowMenuScreen();
		}
		
		private void OnApplyButtonClick() {
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
			
		}
		public void SetDayCycle(bool isNight) {
			
		}

		private void ShowMenuScreen() {
			UIManager.Instance.ShowMenuScreen();
		}
	}
}