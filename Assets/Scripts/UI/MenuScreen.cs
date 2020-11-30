using UnityEngine.UI;
using UnityEngine;

namespace UI {
    public class MenuScreen : MonoBehaviour {

        [SerializeField]
        private Button _playButton;

        [SerializeField]
        private UIManager _uimanager;

        [SerializeField]
        private Button _settingsButton;

        private void OnPlayButtonClick() {
            _uimanager.LoadGameplay();
        }

        private void OnSettingsButtonClick() {
            _uimanager.ShowSettingsScreen();
        }

        private void Awake() {
            _playButton.onClick.AddListener(OnPlayButtonClick);
            _settingsButton.onClick.AddListener(OnSettingsButtonClick);
        }
    }
}