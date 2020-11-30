using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class MenuScreen : MonoBehaviour {

        [SerializeField]
        private Button _playButton;

        [SerializeField]
        private Button _settingsButton;

        [SerializeField]
        private ScriptableIntValue _score;

        private void Awake() {
            _playButton.onClick.AddListener(OnPlayButtonClick);
            _settingsButton.onClick.AddListener(OnSettingsButtonClick);
        }

        private void OnPlayButtonClick() {
            _score.value = 0;
            UIManager.Instance.LoadGameplay();
        }

        private void OnSettingsButtonClick() {
            _score.value = 0;
            UIManager.Instance.ShowSettingsScreen();
        }
    }

}