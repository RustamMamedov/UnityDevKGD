using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace UI {

    public class MenuScreen : MonoBehaviour{

        [SerializeField]
        private Button _play;

        [SerializeField]
        private UIManager _uiManager;

        [SerializeField]
        private Button _settingsButton;

        private void Awake() {
            _play.onClick.AddListener(OnPlayButtonClick);
            _settingsButton.onClick.AddListener(OnSettingsButtonClick);
        }

        private void OnPlayButtonClick() {
            _uiManager.LoadGameplay();
        }

        private void OnSettingsButtonClick() {
            _uiManager.ShowSettingsScreen();
        }
    }
}

