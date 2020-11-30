using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class ToggleScript : MonoBehaviour {

        [SerializeField]
        private Toggle _toggle;

        [SerializeField]
        private Image _leftImage;

        [SerializeField]
        private Image _rightImage;

        private void Awake() {
            _toggle.onValueChanged.AddListener((bool value) => ChangeTogglePosition(_toggle.isOn));

        }

        private void ChangeTogglePosition(bool isOn) {

            if (!isOn) {
                _leftImage.color = Color.white;
                _rightImage.color = Color.red;
            } else {
                _leftImage.color = Color.red;
                _rightImage.color = Color.white;
            }

        }
    }
}