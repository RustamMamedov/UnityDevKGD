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
        private Text _lightLabel;

        [SerializeField]
        private ScriptableBoolValue _isDay;

        private void Awake() {
            
            if(_isDay.value) {
                _lightLabel.text = "Освещение - день";
            } else {
                _lightLabel.text = "Освещение - ночь";
            }
            
            _okButton.onClick.AddListener(OnOkButtonClick);
            _dayButton.onClick.AddListener(OnDayButtonClick);
            _nightButton.onClick.AddListener(OnNightButtonClick);
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



    }
}