using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class MenuScreen : MonoBehaviour {

        [SerializeField]
        private Button _playButton;

        [SerializeField]
        private Button _settingsButon;

        private void Awake() {
            _playButton.onClick.AddListener(OnPlayButtonClick);
            _settingsButon.onClick.AddListener(OnSettingsButtonClick);
        }

        private void OnPlayButtonClick() {
            UIManager.instance.LoadGamePlay();
        }

        private void OnSettingsButtonClick() {
            UIManager.instance.ShowSettingsScreen();
        }
    }
}

