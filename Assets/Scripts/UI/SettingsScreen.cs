using Events;
using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class SettingsScreen : MonoBehaviour {

        [SerializeField]
        private EventDispatcher _gameSavedEventDispatcher;

        [SerializeField]
        private EventDispatcher _settingsChangedEventDispatcher;

        [SerializeField]
        private Button _saveButton;

        [SerializeField]
        private Button _cancelButton;

        [SerializeField]
        private Slider _volume;

        [SerializeField]
        private Text _volumeValueText;

        [SerializeField]
        private Slider _difficulty;

        [SerializeField]
        private Slider _time;

        public float Volume { get { return _volume.value; } }

        public float Difficulty { get { return _difficulty.value; } }

        public float Time { get { return _time.value; } }

        private void Awake() {
            _saveButton.onClick.AddListener(delegate { OnSaveButtonClicked(); });
            _cancelButton.onClick.AddListener(delegate { OnCancelButtonClicked(); });
            _volume.onValueChanged.AddListener(delegate { OnVolumeChanged(); });
        }

        public void SetValues() {
            _volume.value = Save.Settings.volumeValue;
            _difficulty.value = Save.Settings.difficulty == Save.SavedSettings.Difficulty.Hard ? 1f : 0f;
            _time.value = Save.Settings.time == Save.SavedSettings.Time.Night ? 1f : 0f;
            _volumeValueText.text = GetRoundedVolume().ToString() + "%";
        }

        private void OnSaveButtonClicked() {
            _gameSavedEventDispatcher.Dispatch();
            UIManager.instance.CloseSettingsScreen();
        }

        private void OnCancelButtonClicked() {
            SetValues();
        }

        private void OnVolumeChanged() {
            _settingsChangedEventDispatcher.Dispatch();
            _volumeValueText.text = GetRoundedVolume().ToString() + "%";
        }

        private float GetRoundedVolume() {
            return Mathf.Round((_volume.value * 100));
        }
    }
}
