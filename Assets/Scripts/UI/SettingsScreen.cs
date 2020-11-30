using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Audio;
using Game;

namespace UI {

    public class SettingsScreen : MonoBehaviour {

        [SerializeField]
        private Button _okButton;

        [SerializeField]
        private Button _resumeButton;

        private void Awake() {
            _okButton.onClick.AddListener(OnOkButtonClick);
            _resumeButton.onClick.AddListener(OnResumeButtonClick);
        }

        private void OnOkButtonClick() {
            UIManager.Instance.ShowMenuScreen();
        }

        private void OnResumeButtonClick() {
            UIManager.Instance.ShowMenuScreen();
        }
    }
}