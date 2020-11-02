using System;
using UnityEngine;
using UnityEngine.UI;
using UI;

public class MenuScreen : MonoBehaviour {

    [SerializeField]
    private Button _playButton;

    private void Awake() {
        Button playButton = _playButton.GetComponent<Button>();
        playButton.onClick.AddListener(OnPlayButtonClick);
        
    }

    public void OnPlayButtonClick() {
        UIManager.Instance.LoadGameplay();
    }
}

