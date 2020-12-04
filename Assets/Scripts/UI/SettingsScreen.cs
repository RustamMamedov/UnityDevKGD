using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class SettingsScreen : MonoBehaviour {

        [SerializeField]
        private Button _backButton;

        [SerializeField]
        private Button _confirmButton;

        [SerializeField]
        private Slider _soundVolume;

        [SerializeField]
        private Toggle _gamemode;

        [SerializeField]
        private Toggle _daytime;
        private void Awake() {
            _backButton.onClick.AddListener(OnBackButtonClick);
            _confirmButton.onClick.AddListener(OnConfirmButtonClick);

        }

        private void OnBackButtonClick() {
            UIManager.Instance.ShowMenuScreen();
        }

        private void OnConfirmButtonClick() {
            UIManager.Instance.ShowMenuScreen();
        }

        private void OnValueChangedSoundVoulume() {

        }

        private void OnValueChangedGamemode() {

        }
    }
}