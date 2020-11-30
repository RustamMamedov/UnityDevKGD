using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class MenuScreen : MonoBehaviour {

        [SerializeField]
        private Button _playB;

        [SerializeField]
        private Button _SettingB;

        private void Awake() {
            _playB.onClick.AddListener(OnPlayButtonClick);
            _SettingB.onClick.AddListener(OnSettingsButtonClick);
        }

        private void OnPlayButtonClick() {
            UIManager.Instance.LoadGameplay();
        }

        private void OnSettingsButtonClick() {
            UIManager.Instance.ShowSettingsScreen();
        }
    }
}
