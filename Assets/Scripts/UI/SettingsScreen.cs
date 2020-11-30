using UnityEngine;
using UnityEngine.UI;
using Events;
using Game;
using System;

namespace UI {
    public class SettingsScreen : MonoBehaviour {

        [SerializeField]
        private Button _confirmB;

        [SerializeField]
        private Button _cancelB;

        [SerializeField]
        private Dropdown _diff;

        [SerializeField]
        private Dropdown _time;

        [SerializeField]
        private Slider _volume;

        [SerializeField]
        private Text _valueVolume;

        [SerializeField]
        private EventDispatcher _settingsSavedED;

        [SerializeField]
        private ScriptableIntValue _timeCurrent;

        [SerializeField]
        private ScriptableIntValue _diffCurrent;

        [SerializeField]
        private ScriptableFloatValue _volumeCurrent;


        private void Awake() {
            _confirmB.onClick.AddListener(OnConfirmButtonClick);
            _cancelB.onClick.AddListener(OnCancelButtonClick);
            _cancelB.onClick.AddListener(OnCancelButtonClick);
            _volume.onValueChanged.AddListener(delegate { OnChangedVolume(); });
        }

        private void OnEnable() {
            _time.value = _timeCurrent.value;
            _diff.value = _diffCurrent.value;
            _volume.value = _volumeCurrent.value;
        }

        private void OnChangedVolume() {
            _valueVolume.text = $"{Convert.ToInt32(_volume.value * 100)}";
        }

        private void OnConfirmButtonClick() {
            _timeCurrent.value = _time.value;
            _diffCurrent.value = _diff.value;
            _volumeCurrent.value = _volume.value;

            _settingsSavedED.Dispatch();
            UIManager.Instance.ShowMenuScreen();
        }

        private void OnCancelButtonClick() {
            UIManager.Instance.ShowMenuScreen();
        }
    }
}

