using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class MenuScreen : MonoBehaviour {
        
        [SerializeField]
        private Button _playButton;

        private void OnEnable() {
            _playButton.onClick.AddListener(OnPlayButtonClick);
        }

        private void OnDisable() {
            _playButton.onClick.RemoveAllListeners();
        }

        private void OnPlayButtonClick() {
            _playButton.onClick.RemoveAllListeners();
            UIManager.Instance.LoadGameplay();
        }
    }
}