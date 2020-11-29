using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game;
namespace UI {

    public class SettingsScreen : MonoBehaviour {

        [SerializeField]
        private Slider _volumeSlider;

        [SerializeField]
        private Toggle _difficultyToggle;

        [SerializeField]
        private Image _FirstDifficultyImage;

        [SerializeField]
        private Image _SecondDifficultyImage;

        [SerializeField]
        private Toggle _lightToggle;

        [SerializeField]
        private Image _FirstLightImage;

        [SerializeField]
        private Image _SecondLightImage;

        [SerializeField]
        private Button _acceptButton;

        [SerializeField]
        private Button _cancelButton;

        private const float VOLUME_DEFAULT = 0.5f;
        private const int DIFFICULT_DEFAULT = 0;
        private const int LIGHT_DEFAULT = 0;

        private void Awake() {
            SetDefaultForFirstTime();
            GetSettings();
            _difficultyToggle.onValueChanged.AddListener(DifficultyToggle);
            _lightToggle.onValueChanged.AddListener(LightToggle);
            _acceptButton.onClick.AddListener(Accept);
            _cancelButton.onClick.AddListener(Cancel);
        }
        private void SetDefaultForFirstTime() {
            if (!PlayerPrefs.HasKey(DataKeys.VOLUME_KEY)) {
                PlayerPrefs.SetFloat(DataKeys.VOLUME_KEY, VOLUME_DEFAULT);
                _volumeSlider.value = VOLUME_DEFAULT;
            }

            if (!PlayerPrefs.HasKey(DataKeys.DIFFICULT_KEY)) {
                PlayerPrefs.SetFloat(DataKeys.DIFFICULT_KEY, DIFFICULT_DEFAULT);
                _difficultyToggle.isOn = (DIFFICULT_DEFAULT == 1);
            }

            if (!PlayerPrefs.HasKey(DataKeys.LIGHT_KEY)) {
                PlayerPrefs.SetFloat(DataKeys.LIGHT_KEY, LIGHT_DEFAULT);
                _lightToggle.isOn = (LIGHT_DEFAULT == 1);
            }
        }

        private void GetSettings() {
            _volumeSlider.value = PlayerPrefs.GetFloat(DataKeys.VOLUME_KEY);
            _difficultyToggle.isOn = PlayerPrefs.GetInt(DataKeys.DIFFICULT_KEY) == 1;
            _lightToggle.isOn = PlayerPrefs.GetInt(DataKeys.LIGHT_KEY) == 1;
        }

        private void SetSettings() {
            PlayerPrefs.SetFloat(DataKeys.VOLUME_KEY, _volumeSlider.value);
            PlayerPrefs.SetInt(DataKeys.DIFFICULT_KEY, _difficultyToggle.isOn == true ? 1 : 0);
            PlayerPrefs.SetInt(DataKeys.LIGHT_KEY, _lightToggle.isOn == true ? 1 : 0);
        }

        private void DifficultyToggle(bool value) {
            if(value == false) {
                EasyMode();
            } else {
                HardMode();
            }
        }

        private void EasyMode() {
            _FirstDifficultyImage.color = Color.green;
            _SecondDifficultyImage.color = Color.red;
        }

        private void HardMode() {
            _FirstDifficultyImage.color = Color.red;
            _SecondDifficultyImage.color = Color.green;
        }

        private void LightToggle(bool value) {
            if (value == false) {
                DayMode();
            } else {
                NightMode();
            }
        }

        private void DayMode() {
            _FirstLightImage.color = Color.green;
            _SecondLightImage.color = Color.red;
        }

        private void NightMode() {
            _FirstLightImage.color = Color.red;
            _SecondLightImage.color = Color.green;
        }

        private void Accept() {
            SetSettings();
            UIManager.Instance.ShowMenuScreen();
        }

        private void Cancel() {
            GetSettings();
            UIManager.Instance.ShowMenuScreen();
        }

    }

}
