using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game;

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
            UIManager.Instance.ShowSettingsScreen();
        }

        private void OnPlayButtonClick() {
            UIManager.Instance.LoadGameplay();
        }
    }
}

