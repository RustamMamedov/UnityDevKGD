using System;
using UnityEngine;
using UnityEngine.UI;
using UI;

public class MenuScreen : MonoBehaviour {

    [SerializeField]
    private Button _playButton;

    [SerializeField]
    private Button _settingsButton;

    private void Awake() {
        _playButton.onClick.AddListener(OnPlayButtonClick);
        _settingsButton.onClick.AddListener(OnSettingsButtonClick);

    }

    public void OnPlayButtonClick() {
        UIManager.Instance.LoadGameplay();
    }

    public void OnSettingsButtonClick() {
        UIManager.Instance.ShowSettingsScreen();
    }
}

