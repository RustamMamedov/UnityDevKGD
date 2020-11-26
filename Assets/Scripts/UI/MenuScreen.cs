using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class MenuScreen : MonoBehaviour {

        [SerializeField]
        private Button _playButton;

        [SerializeField]
        private Button _settingsButton;

        [SerializeField]
        private SettingsScreen _settingsScreen;

        private void Awake() {
            _playButton.onClick.AddListener(delegate { OnPlayButtonClick(); });
            _settingsButton.onClick.AddListener(delegate { OnSettingsButtonClick(); });
        }

        private void OnPlayButtonClick() {
            UIManager.instance.LoadGameplay();
        }

        private void OnSettingsButtonClick() {
            _settingsScreen.SetValues();
            if (!UIManager.instance.IsSettingsActive()) {
                UIManager.instance.ShowSettingsScreen();
            } else {
                UIManager.instance.CloseSettingsScreen();
            }
        }

    }

}
