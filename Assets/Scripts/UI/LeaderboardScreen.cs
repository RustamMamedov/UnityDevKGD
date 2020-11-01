using UnityEngine;
using UnityEngine.UI;

namespace UI {
    
    public class LeaderboardScreen : MonoBehaviour {

        // Fields.

        [SerializeField]
        private Button _menuButton;


        // Life cycle.

        private void Awake() {
            _menuButton.onClick.AddListener(OnMenuButtonClick);
        }


        // Event handling.

        private void OnMenuButtonClick() {
            UIManager.Instance.LoadMenuScene();
        }


    }

}
