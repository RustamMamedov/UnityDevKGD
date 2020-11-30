using Audio;
using Game;
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
        private Slider _difficultySlider;

        [SerializeField]
        private Slider _lightSlider;

        [SerializeField]
        private Button _applyButton;

        [SerializeField]
        private Button _cancelButton;

        private int _difficulty;
        private float _volume;
        private int _light;

        public void SetVolume() {
            foreach (var source in _audioSources) {
                source.volume = _volumeSlider.value;
                _volume = _volumeSlider.value;
            }
        }

        private void SetDifficulty() {
            _difficulty = (int)_difficultySlider.value;
        }

        private void SetLight() {
            _light = (int)_lightSlider.value;
        }
        
        private void SaveToPlayerPrefs() {
            PlayerPrefs.SetFloat("Volume", _volume);
            PlayerPrefs.SetInt("Difficulty", _difficulty);
            PlayerPrefs.SetInt("Light", _light);
        }

        private void LoadFromPlayerPrefs() {
            _volume = PlayerPrefs.GetFloat("Volume");
            _difficulty = PlayerPrefs.GetInt("Difficulty");
            _light = PlayerPrefs.GetInt("Light");
            _volumeSlider.value = _volume;
            _difficultySlider.value = _difficulty;
            _lightSlider.value = _light;
        }

        private void Awake() {
            LoadFromPlayerPrefs();
            _applyButton.onClick.AddListener(OnApplyButtonClick);
            _cancelButton.onClick.AddListener(OnCancelButtonClick);
            _volumeSlider.onValueChanged.AddListener(delegate { SetVolume(); });
            _difficultySlider.onValueChanged.AddListener(delegate { SetDifficulty(); });
            _lightSlider.onValueChanged.AddListener(delegate { SetLight(); });
        }

        private void OnApplyButtonClick() {
            SaveToPlayerPrefs();
            UIManager.Instance.LoadMenu();
        }

        private void OnCancelButtonClick() {
            LoadFromPlayerPrefs();
            UIManager.Instance.LoadMenu();
        }

    }
}
