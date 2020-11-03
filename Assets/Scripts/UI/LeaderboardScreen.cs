using System;
using UnityEngine;
using UnityEngine.UI;
using UI;
using Game;

namespace UI {
    public class LeaderboardScreen : MonoBehaviour {

        [SerializeField]
        private Button _menuButton;

        private void Awake() {
            _menuButton.onClick.AddListener(OnMenuButtonClick);

        }

        public void OnMenuButtonClick() {
            UIManager.Instance.LoadMenu();
        }

    }
}