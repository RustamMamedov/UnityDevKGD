using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class MenuScreen : MonoBehaviour {

        [SerializeField]
        private Button _playB;

        private void Awake() {
            _playB.onClick.AddListener(OnPlayButtonClick);
        }

        private void OnPlayButtonClick() {
            UIManager.Instance.LoadGameplay();
        }
    }
}
