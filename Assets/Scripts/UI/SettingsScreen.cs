using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class SettingsScreen : MonoBehaviour {

        [SerializeField]
        private Button _ConfirmB;

        [SerializeField]
        private Button _CancelB;

        private void Awake() {
            _ConfirmB.onClick.AddListener(OnConfirmButtonClick);
            _CancelB.onClick.AddListener(OnCancelButtonClick);
        }

        private void OnConfirmButtonClick() {
            UIManager.Instance.ShowMenuScreen();
        }

        private void OnCancelButtonClick() {
            UIManager.Instance.ShowMenuScreen();
        }
    }
}

