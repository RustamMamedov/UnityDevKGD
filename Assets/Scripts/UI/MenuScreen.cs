using Audio;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class MenuScreen : MonoBehaviour {

        [SerializeField]
        private Button _playButton;

        private void Awake() {
            _playButton.onClick.AddListener(OnPlayButtonClick);
        }

        private void OnPlayButtonClick() {
            MusicManager.Instance.PlayGameplayMusic();
            UIManager.Instance.LoadGameplay();
        }
    }
}