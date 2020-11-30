using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game;

namespace UI {
    
    public class OptionScreen : MonoBehaviour {

        [SerializeField]
        private UIManager _uIManager;

        [SerializeField]
        private Slider _volumeSlider;

        [SerializeField]
        private Slider _gameModeSlider;

        [SerializeField]
        private Slider _timeModeSlider;

        [SerializeField]
        private ScriptableFloatValue _volume;

        [SerializeField]
        private ScriptableIntValue _gameMode;

        [SerializeField]
        private ScriptableIntValue _timeMode;

        [SerializeField]
        private Button _ok;

        [SerializeField]
        private Button _cansle;

        private void OnEnable() {
            _ok.onClick.AddListener(OnOkButton);
            _cansle.onClick.AddListener(OnCansleButton);
            _volumeSlider.value = _volume.value;
            _gameModeSlider.value = _gameMode.value;
            _timeModeSlider.value = _timeMode.value;
        }

        private void OnDisable() {
            _ok.onClick.RemoveListener(OnOkButton);
            _cansle.onClick.RemoveListener(OnCansleButton);
        }

        private void OnOkButton() {
            _volume.value = _volumeSlider.value;
            _gameMode.value = (int)_gameModeSlider.value;
            _timeMode.value = (int)_timeModeSlider.value;
            _uIManager.ShowMenuScreen();
        }

        private void OnCansleButton() {
            _volumeSlider.value = _volume.value;
            _gameModeSlider.value = _gameMode.value;
            _timeModeSlider.value = _timeMode.value;
            _uIManager.ShowMenuScreen();
        }

    }
}