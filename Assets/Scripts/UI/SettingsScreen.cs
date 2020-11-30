using Events;
using Managers;
using Sirenix.OdinInspector;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;
using Values;

namespace UI {
    
    public class SettingsScreen : MonoBehaviour {

        // Fields.

        [BoxGroup("Settings values")]
        [SerializeField]
        private ScriptableFloatValue _volumeValue;

        [BoxGroup("Settings values")]
        [SerializeField]
        private ScriptableIntValue _daytimeValue;

        [BoxGroup("Settings values")]
        [SerializeField]
        private ScriptableIntValue _difficultyValue;

        [BoxGroup("Input receivers")]
        [SerializeField]
        private Slider _volumeSlider;

        [BoxGroup("Input receivers")]
        [SerializeField]
        private ButtonSlider _daytimeSlider;

        [BoxGroup("Input receivers")]
        [SerializeField]
        private ButtonSlider _difficultySlider;

        [BoxGroup("Input receivers")]
        [SerializeField]
        private Button _cancelButton;

        [BoxGroup("Input receivers")]
        [SerializeField]
        private Button _okButton;

        [BoxGroup("Event dispatchers")]
        [SerializeField]
        private EventDispatcher _volumeChangedDispatcher;

        [BoxGroup("Event dispatchers")]
        [SerializeField]
        private EventDispatcher _daytimeChangedDispatcher;

        [BoxGroup("Event dispatchers")]
        [SerializeField]
        private EventDispatcher _difficultyChangedDispatcher;


        // Life cycle.

        private void Start() {
            ResetShowed();
            _volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
            _daytimeSlider.ChoiceMade += OnDaytimeChosen;
            _difficultySlider.ChoiceMade += OnDifficultyChosen;
            _cancelButton.onClick.AddListener(OnCancel);
            _okButton.onClick.AddListener(OnApply);
        }


        // Event handling.

        private void OnVolumeChanged(float volume) {
            _volumeValue.value = volume;
            _volumeChangedDispatcher.Dispatch();
        }

        private void OnDaytimeChosen(int daytime) {
            _daytimeValue.value = daytime;
            _daytimeChangedDispatcher.Dispatch();
        }

        private void OnDifficultyChosen(int difficulty) {
            _difficultyValue.value = difficulty;
            _difficultyChangedDispatcher.Dispatch();
        }

        private void OnCancel() {
            SettingsManager.Instance.Revert();
            ResetShowed();
            Close();
        }

        private void OnApply() {
            SettingsManager.Instance.Save();
            Close();
        }


        // Support methods.

        private void ResetShowed() {
            _volumeSlider.value = _volumeValue.value;
            _daytimeSlider.SetChoice(_daytimeValue.value);
            _difficultySlider.SetChoice(_difficultyValue.value);
        }

        private void Close() {
            UIManager.Instance.HideAllScreens();
            UIManager.Instance.ShowMenuScreen();
        }

    
    }

}
