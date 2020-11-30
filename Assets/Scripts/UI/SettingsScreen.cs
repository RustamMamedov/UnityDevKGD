using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using Game; 

namespace UI {
    public class SettingsScreen : Settings {
        
        [SerializeField] 
        private Button _saveButton;

        [SerializeField] 
        private Button _cancelButton;

        [SerializeField]
        private Slider _volumeSlider;

        [SerializeField]
        private Slider _difficultySlider;

        [SerializeField]
        private Slider _timeDaySlider;

        [SerializeField]
        private ScriptableFloatValue _valDifficulty;

        [SerializeField]
        private ScriptableIntValue _valDay;
        
        [SerializeField]
        private AudioMixer AudioMixer;

        public void AudioVolume (float sliderValue) {
            AudioMixer.SetFloat("SettingsVolume", sliderValue);
        }

       /* private void Awake() { 
            _saveButton.onClick.AddListener(OnSaveButtonClick); 
        } 
        private void OnSaveButtonClick() { 
            UIManager.Instance.LoadMenu(); 
        } */

        private void OnEnable() { 
            _timeDaySlider.value = _valDay.value; 
            _difficultySlider.value = _valDifficulty.value; 
 
            _saveButton.onClick.AddListener(OnSaveButttonClick); 
            _cancelButton.onClick.AddListener(OnCancelButttonClick);  
        }

        private void OnSaveButttonClick() { 
            _valDay.value = (int)_timeDaySlider.value; 
            _valDifficulty.value = (int)_difficultySlider.value;  
            UIManager.Instance.LoadMenu();      
        } 
 
        private void OnCancelButttonClick() { 
            UIManager.Instance.LoadMenu(); 
         
        } 

    }

}
