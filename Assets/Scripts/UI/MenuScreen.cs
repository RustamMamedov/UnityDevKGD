using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class MenuScreen : MonoBehaviour {

        [SerializeField]
        private Button _playClickButton;

        [SerializeField]
        private UIManager _uiManager;

        private void Awake() {
            _playClickButton.onClick.AddListener(OnPlayButtonClick);
        }

        private void OnPlayButtonClick() {
            _uiManager.LoadGamePlay();
        }
    }
}
