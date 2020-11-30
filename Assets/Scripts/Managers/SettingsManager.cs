using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;

namespace Game {

    public class SettingsManager : MonoBehaviour {

        [SerializeField] 
        private ScriptableFloatValue _musicVolumeScriptableFloatValue;
		
        [SerializeField] 
        private ScriptableFloatValue _gameVolumeScriptableFloatValue;

        [SerializeField] 
        private ScriptableBoolValue _isNightScriptableBoolValue;
		
        [SerializeField] 
        private ScriptableBoolValue _isHardScriptableBoolValue;
		
        [SerializeField] 
        private ScriptableBoolValue _isCrazyModeScriptableBoolValue;
        
        [SerializeField] 
        private EventListener _settingsGotSavedEventListener;

        private void OnEnable() {
            LoadFromPlayerPrefs();
            _settingsGotSavedEventListener.OnEventHappened += SaveToPlayerPrefs;
        }
        
        private void OnDisable() {
            _settingsGotSavedEventListener.OnEventHappened -= SaveToPlayerPrefs;
        }
        
        private void SaveToPlayerPrefs() {
            PlayerPrefs.SetFloat("MusicVolume", _musicVolumeScriptableFloatValue.value);
            PlayerPrefs.SetFloat("GameVolume", _gameVolumeScriptableFloatValue.value);
            PlayerPrefs.SetString("IsHard", _isHardScriptableBoolValue.value.ToString());
            PlayerPrefs.SetString("IsNight", _isNightScriptableBoolValue.value.ToString());
            PlayerPrefs.SetString("IsCrazy", _isCrazyModeScriptableBoolValue.value.ToString());
        }

        private void LoadFromPlayerPrefs() {
            if (!PlayerPrefs.HasKey("MusicVolume") || !PlayerPrefs.HasKey("GameVolume") || !PlayerPrefs.HasKey("IsHard") || !PlayerPrefs.HasKey("IsNight") || !PlayerPrefs.HasKey("IsCrazy")) {
                return;
            }
            _musicVolumeScriptableFloatValue.value = PlayerPrefs.GetFloat("MusicVolume");
            _gameVolumeScriptableFloatValue.value = PlayerPrefs.GetFloat("GameVolume");
            var isHardString = PlayerPrefs.GetString("IsHard");
            _isHardScriptableBoolValue.value = ConvertPrefsToBool(isHardString);
            var isNightString = PlayerPrefs.GetString("IsNight");
            _isNightScriptableBoolValue.value = ConvertPrefsToBool(isNightString);
            var isCrazyString = PlayerPrefs.GetString("IsCrazy");
            _isCrazyModeScriptableBoolValue.value = ConvertPrefsToBool(isCrazyString);
        }
        
        private bool ConvertPrefsToBool(string value) {
            if (value.ToLower() == "true") {
                return true;
            }
            return false;
        }
    }
}