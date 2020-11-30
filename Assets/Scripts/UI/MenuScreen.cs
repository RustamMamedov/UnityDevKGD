using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class MenuScreen : MonoBehaviour {

        [SerializeField]
        private Button _playButton;

        [SerializeField]
        private Button _settingsButton;
        private void Awake() {

            _playButton.onClick.AddListener(OnPlayButtonClick);
            _settingsButton.onClick.AddListener(OnSettingsButtonClick);
        }

        private void OnSettingsButtonClick() {
            UIManager.Instansce.ShowSettingsScreen();
        }

        private void OnPlayButtonClick() {

            UIManager.Instansce.LoadGameplay();
        }
    }
}

