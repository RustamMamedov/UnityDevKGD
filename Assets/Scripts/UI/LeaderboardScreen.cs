using UnityEngine.UI;
using UnityEngine;

namespace UI {
    public class LeaderboardScreen : MonoBehaviour {

        [SerializeField]
        private Button _menuButton;

        [SerializeField]
        private UIManager _uimanager;

        private void OnMenuButtonClick() {
            _uimanager.LoadMenu();
        }

        private void Awake() {
            _menuButton.onClick.AddListener(OnMenuButtonClick);
        }
    }
}