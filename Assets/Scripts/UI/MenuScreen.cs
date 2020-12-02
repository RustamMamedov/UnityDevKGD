using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class MenuScreen : MonoBehaviour {
        
        [SerializeField]
        private Button _playButton;

        [SerializeField]
        private Button _settingsButton;

        private void OnEnable() {
            _playButton.onClick.AddListener(OnPlayButtonClick);
            _settingsButton.onClick.AddListener(OnSettingsButtonClick);
        }

        private void OnDisable() {
            _playButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();
        }

        private void OnPlayButtonClick() {
            _playButton.onClick.RemoveAllListeners();
            UIManager.Instance.LoadGameplay();
        }

        private void OnSettingsButtonClick() {
            _settingsButton.onClick.RemoveAllListeners();
            UIManager.Instance.ShowSettingsScreen();
        }
    }
}