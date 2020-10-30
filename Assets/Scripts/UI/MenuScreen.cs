using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class MenuScreen : MonoBehaviour {

        [SerializeField]
        private Button _playButton;

        private void Awake() {
            UIManager.instance.ShowMenuScreen();
            _playButton.onClick.AddListener(OnPlayButtonClick);
        }

        private void OnPlayButtonClick() {
            UIManager.instance.LoadGameplay();
        }

    }

}
