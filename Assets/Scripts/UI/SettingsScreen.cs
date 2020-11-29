using Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class SettingsScreen : MonoBehaviour {

        [SerializeField]
        private Slider _volumeSlider;

        [SerializeField]
        private List<AudioSource> _audioSources = new List<AudioSource>();

        [SerializeField]
        private Scrollbar _difficultyScrollbar;

        [SerializeField]
        private Scrollbar _lightScrollbar;

        [SerializeField]
        private Button _applyButton;

        [SerializeField]
        private Button _cancelButton;

        public void SetLevel() {
            foreach (var source in _audioSources) {
                source.volume = _volumeSlider.value;
            }
        }

        private void Awake() {
            _volumeSlider.value = 0.5f;
            _applyButton.onClick.AddListener(OnApplyButtonClick);
            _cancelButton.onClick.AddListener(OnCancelButtonClick);
            _volumeSlider.onValueChanged.AddListener(delegate { SetLevel(); } );
        }

        private void OnApplyButtonClick() {
            UIManager.Instance.LoadMenu();
        }

        private void OnCancelButtonClick() {
            UIManager.Instance.LoadMenu();
        }

    }
}
