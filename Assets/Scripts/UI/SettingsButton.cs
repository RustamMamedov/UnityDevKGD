using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class SettingsButton : MonoBehaviour {

        [SerializeField]
        private Button _button;

        [SerializeField]
        private ButtonType _buttonType;

        private enum ButtonType {
            SettingsButton,
            OKButton,
            CancelButton,
        }

        private void Awake() {
            switch (_buttonType) {
                case ButtonType.SettingsButton:
                    _button.onClick.AddListener(delegate { OnSettingsButtonClick(); });
                    break;
                case ButtonType.OKButton:
                    _button.onClick.AddListener(delegate { OnOkButtonClick(); });
                    break;
                case ButtonType.CancelButton:
                    _button.onClick.AddListener(delegate { OnCancelButtonClick(); });
                    break;
            }
        }

        private void OnSettingsButtonClick() {
            if (!UIManager.instance.IsSettingsActive()) {
                UIManager.instance.ShowSettingsScreen();
            } else {
                UIManager.instance.CloseSettingsScreen();
            }
        }

        private void OnOkButtonClick() {

        }

        private void OnCancelButtonClick() {

        }
    }
}
