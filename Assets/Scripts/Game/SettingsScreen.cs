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
		private AudioMixer _audioMixer;
		
		private void Awake() {
			_cancelButton.onClick.AddListener(OnCancelButtonClick);
		}

		private void OnCancelButtonClick() {
			UIManager.Instance.ShowMenuScreen();
		}

		public void SetMusicVolume(float volume) {
			_audioMixer.SetFloat("MusicVolume", volume);
		}

		public void SetDifficultyLevel(bool isHard) {
			
		}
		public void SetDayCycle(bool isNight) {
			
		}
	}
}