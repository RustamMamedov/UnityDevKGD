using UnityEngine;
using UnityEngine.UI;


namespace UI {
    public class LeaderBoardScreen : MonoBehaviour {

        [SerializeField]
        private Button _menuButton;

        private void Awake() {
            _menuButton.onClick.AddListener(OnMenuButtonClick);
        }

        private void OnDestroy() {
            _menuButton.onClick.RemoveListener(OnMenuButtonClick);
        }

        private void OnMenuButtonClick() {
            UIManager.Instance.LoadMenu();
        }
    }
}