using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class SettingsScreen : MonoBehaviour {

        [SerializeField]
        private Slider _volumeSlider;
        [SerializeField]
        private List<AudioSource> _audioSources;

        [SerializeField]
        private Toggle _difficultToggle;

        [SerializeField]
        private Toggle _ligthToggle;

        [SerializeField]
        private Button _saveButton;

        [SerializeField]
        private Button _cancelButton;

        [SerializeField]
        private GameObject _settingsScreen;

        private float _sliderVolumeValue;

        private const string RECORDS_KEY = "settings";

        private void Awake() {
            _saveButton.onClick.AddListener(Save);
        }

        private void SetSoundVolume() {
            _sliderVolumeValue = _volumeSlider.value;
            for (int i = 0; i < _audioSources.Count; i++) {
                _audioSources[i].volume = _sliderVolumeValue;
            }
        }

        private void Save(){

            SetSoundVolume();
            UIManager.Instance.ShowMenuScreen();
        }


    }
}