using UnityEngine;
using UnityEngine.UI;
using Game;

namespace UI {

    public class SettingsSaves : MonoBehaviour {

        [SerializeField]
        private Slider _volumeSlider;

        [SerializeField]
        private ScriptableFloatValue _volume;

        [SerializeField]
        private Slider _complexitySlider;

        [SerializeField]
        private ScriptableIntValue _complexity;

        [SerializeField]
        private Slider _illuminationSlider;

        [SerializeField]
        private ScriptableIntValue _illumination;

        [SerializeField]
        private Button _accept;

        [SerializeField]
        private Button _cancel;

        [SerializeField]
        private UIManager _uIManager;

        private void SaveChanges() {
            _volume.value = _volumeSlider.value;
            _complexity.value = (int)_complexitySlider.value;
            _illumination.value = (int)_illuminationSlider.value;
            _uIManager.ShowMenuScreen();
        }

        private void DiscardChanges() {
            _volumeSlider.value = _volume.value;
            _complexitySlider.value = _complexity.value;
            _illuminationSlider.value = _illumination.value;
            _uIManager.ShowMenuScreen();
        }

        private void Awake() {
            _accept.onClick.AddListener(SaveChanges);
            _cancel.onClick.AddListener(DiscardChanges);
            _volume.value = .5f;
        }
    }
}
