using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class SettingsScreen : MonoBehaviour {
        
        [SerializeField]
        private Button _okButton;

        [SerializeField]
        private Button _dayButton;

        [SerializeField]
        private Button _nightButton;

        [SerializeField]
        private Button _easyModeButton;

        [SerializeField]
        private Button _hardModeButton;

        [SerializeField]
        private Text _lightLabel;

        [SerializeField]
        private Text _gameModeLabel;

        [SerializeField]
        private ScriptableBoolValue _isDay;

        [SerializeField]
        private ScriptableBoolValue _isHard;

        private void Awake() {

            if(_isHard.value) {
                _gameModeLabel.text = "Сложность - сложно";
            } else {
                _gameModeLabel.text = "Сложность - легко";
            }

            if(_isDay.value) {
                _lightLabel.text = "Освещение - день";
            } else {
                _lightLabel.text = "Освещение - ночь";
            }
            
            _okButton.onClick.AddListener(OnOkButtonClick);
            _dayButton.onClick.AddListener(OnDayButtonClick);
            _nightButton.onClick.AddListener(OnNightButtonClick);
            _hardModeButton.onClick.AddListener(OnHardModeButtonClick);
            _easyModeButton.onClick.AddListener(OnEasyModeButtonClick);
        }

        private void OnOkButtonClick() {
            UIManager.Instance.ShowMenuScreen();

        }

        private void OnDayButtonClick() {
            _isDay.value = true;
            _lightLabel.text = "Освещение - день";
        }

        private void OnNightButtonClick() {
            _isDay.value = false;
            _lightLabel.text = "Освещение - ночь";
        }

        private void OnEasyModeButtonClick() {
            _isHard.value = false;
            _gameModeLabel.text = "Сложность - легко ";
        }

        private void OnHardModeButtonClick() {
            _isHard.value = true;
            _gameModeLabel.text = "Сложность - сложно";
        }

    }
}