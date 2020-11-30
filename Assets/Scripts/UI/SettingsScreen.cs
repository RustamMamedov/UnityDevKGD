using Events;
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

        [BoxGroup("Components")]
        [SerializeField]
        private EventDispatcher _volumeChangedDispatcher;


        // Life cycle.

        private void Start() {
            _volumeSlider.value = _volumeValue.value;
            _volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
            _daytimeSlider.SetChoice(_daytimeValue.value);
            _daytimeSlider.ChoiceMade += OnDaytimeChosen;
            _difficultySlider.SetChoice(_difficultyValue.value);
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
        }

        private void OnDifficultyChosen(int difficulty) {
            _difficultyValue.value = difficulty;
        }

        private void OnCancel() {
            Revert();
            Close();
        }

        private void OnApply() {
            Save();
            Close();
        }


        // Support methods.

        private void Revert() {
            //
            _volumeChangedDispatcher.Dispatch();
        }

        private void Save() {
            //
        }

        private void Close() {
            UIManager.Instance.HideAllScreens();
            UIManager.Instance.ShowMenuScreen();
        }

    
    }

}
