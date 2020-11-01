using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class LeaderBoardScreen : MonoBehaviour {

        [SerializeField]
        private Button _menuButton;

        private void Awake() {
            _menuButton.onClick.AddListener(OnPlayButtonClick);
        }

        private void OnPlayButtonClick() {
            UIManager.Instance.LoadMenu();
        }

    }
}
