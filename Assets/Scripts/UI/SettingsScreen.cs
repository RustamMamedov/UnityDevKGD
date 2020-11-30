using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace UI {
    public class SettingsScreen : Settings {
        
        [SerializeField] 
        private Button _saveButton;

        [SerializeField] 
        private Button _cancelButton;

        [SerializeField]
        private AudioMixer AudioMixer;

        public void AudioVolume (float sliderValue) {
            AudioMixer.SetFloat("SettingsVolume", sliderValue);
        }

        private void Awake() { 
            _saveButton.onClick.AddListener(OnSaveButtonClick); 
        } 
        private void OnSaveButtonClick() { 
            UIManager.Instance.LoadMenu(); 
        }

    

    }

}
