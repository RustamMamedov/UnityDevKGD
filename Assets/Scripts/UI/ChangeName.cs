using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class ChangeName : MonoBehaviour {

        [SerializeField]
        private Slider _slider;

        [SerializeField]
        private Text _text;

        [SerializeField]
        private string _zeroWord;        
        
        [SerializeField]
        private string _oneWord;

        public void ChangeNameButton() {
            if (_slider.value == 0f) {
                _text.text = _zeroWord;
            } else {
                _text.text = _oneWord;
            }
        }

        private void Awake() {
            ChangeNameButton();
        }
    }
}