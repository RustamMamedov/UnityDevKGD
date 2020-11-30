using System.Collections; 
using System.Collections.Generic;
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
        private Button _cancelButton;

        [SerializeField]
        private Slider _volumeSlider;

        [SerializeField]
        private Text _lightLabel;

        [SerializeField]
        private Text _gameModeLabel;

        [SerializeField]
        private List<GameObject> _audiosList = new List<GameObject>();

        [SerializeField]
        private ScriptableBoolValue _isDay;

        [SerializeField]
        private ScriptableBoolValue _isHard;

        private bool _isDaySave;

        private bool _isHardSave;

        private string _lightLabelSave;

        private string _gameModeLabelSave;

        private List<float> _audioVolumeSave = new List<float>();



        private void Awake() {

            SettingsSaver();

            _audiosList[0].GetComponent<AudioSource>().volume = _audiosList[1].GetComponent<AudioSource>().volume;
            _volumeSlider.value = _audiosList[1].GetComponent<AudioSource>().volume;

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
            _cancelButton.onClick.AddListener(OnCancelButtonClick);
            _volumeSlider.onValueChanged.AddListener(delegate {OnSliderValueChange();});
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

        private void OnSliderValueChange() {
            
            for(int i = 0; i < _audiosList.Count; i++) {
                _audiosList[i].GetComponent<AudioSource>().volume = _volumeSlider.value;
            }

        }

        private void OnCancelButtonClick() {
            _isHard.value = _isHardSave;
            _isDay.value = _isDaySave;
            _lightLabel.text = _lightLabelSave;
            _gameModeLabel.text = _gameModeLabelSave;

            for(int i = 0; i < _audiosList.Count; i++) {
                _audiosList[i].GetComponent<AudioSource>().volume = _audioVolumeSave[i];
            }

            UIManager.Instance.ShowMenuScreen();

        }

        private void SettingsSaver() {
            _isHardSave = _isHard.value;
            _isDaySave = _isDay.value;
            _lightLabelSave = _lightLabel.text;
            _gameModeLabelSave = _gameModeLabel.text;

            for(int i = 0; i < _audiosList.Count; i++) {
                _audioVolumeSave.Add(_audiosList[i].GetComponent<AudioSource>().volume);
            }

        }
    }
}