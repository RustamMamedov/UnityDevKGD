using UnityEngine;
using UnityEngine.UI;

namespace UI {
    
    public class MenuScreen : MonoBehaviour {

        // Fields.

        [SerializeField]
        private Button _playButton;

        [SerializeField]
        private Button _settingsButton;


        // Life cycle.

        private void Start() {
            _playButton.onClick.AddListener(OnPlayButtonClick);
            _settingsButton.onClick.AddListener(OnSettingsButtonClick);
        }


        // Event handling.

        private void OnPlayButtonClick() {
            UIManager.Instance.LoadGameplayScene();
        }

        private void OnSettingsButtonClick() {
            UIManager.Instance.HideAllScreens();
            UIManager.Instance.ShowSettingsScreen();
        }


    }

}
