using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class LeaderboardScreen : MonoBehaviour {

        [SerializeField]
        private Button _menuButton;

        private void Awake() {
            _menuButton.onClick.AddListener(OnClickMenuButton);
        }

        private void OnClickMenuButton() {
            UIManager.instance.LoadMenu();
        }
    }
}
