using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class ButtonSlider : MonoBehaviour {

        // Delegates.

        public delegate void ChoiceMadeHandler(int newChoice);


        // Events.

        public event ChoiceMadeHandler ChoiceMade;


        // Fields.

        [SerializeField]
        private Button[] _buttons;

        private int _currentChoice = 0;


        // Life cycle.

        private void Start() {
            for (int i = 0; i < _buttons.Length; ++i) {
                var button = _buttons[i];
                button.interactable = (i != _currentChoice);
                int choice = i;
                button.onClick.AddListener(() => OnButtonClick(choice));
            }
        }


        // Event handling.

        private void OnButtonClick(int index) {
            SetChoice(index);
            ChoiceMade?.Invoke(index);
        }


        // Public methods.

        public void SetChoice(int newChoice) {
            if (_currentChoice >= 0 && _currentChoice < _buttons.Length) {
                _buttons[_currentChoice].interactable = true;
            }
            _currentChoice = newChoice;
            if (_currentChoice >= 0 && _currentChoice < _buttons.Length) {
                _buttons[_currentChoice].interactable = false;
            }
        }


    }

}
