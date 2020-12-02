using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game;

namespace UI {

    public class MenuScreen : MonoBehaviour {

        [SerializeField]
        private ScriptableIntValue _currentScore;

        [SerializeField]
        private Button _playButton;

        [SerializeField]
        private Button _settingsButton;

        private void Awake() {
            _playButton.onClick.AddListener(OnPlayButtonClick);
            _settingsButton.onClick.AddListener(OnSettingsButtonClick);
        }
        public void OnPlayButtonClick() {
            _currentScore.value = 0;
            UIManager.Instance.LoadGameplay();
        }
        public void OnSettingsButtonClick() {
            UIManager.Instance.ShowSettingsScreen();
        }
    }
}
