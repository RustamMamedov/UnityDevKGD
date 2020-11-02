using UnityEngine.UI;
using UnityEngine;

namespace UI {
    public class MenuScreen : MonoBehaviour {

        [SerializeField]
        private Button _playButton;

        [SerializeField]
        private UIManager _uimanager;

        private void OnPlayButtonClick() {
            _uimanager.LoadGameplay();
        }

        private void Awake() {
            _playButton.onClick.AddListener(OnPlayButtonClick);
        }
    }
}