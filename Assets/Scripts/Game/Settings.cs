using System;
using Events;
using UnityEngine;

namespace Game {
    public class Settings : MonoBehaviour {

        [SerializeField]
        private EventListener _settingsChangedListener;

        [SerializeField]
        private ScriptableFloatValue _volumeAsset;
        
        [SerializeField]
        private ScriptableBoolValue _difficultAsset;
        
        [SerializeField]
        private ScriptableBoolValue _nightAsset;

        [SerializeField] 
        private EventDispatcher _settingsLoaded;

        private class SavedDataWrapper {

            public float volume;
            public bool difficult;
            public bool night;
        }
        
        private const string SETTINGS_KEY = "Settings";

        private void Awake() {
            _settingsChangedListener.OnEventHappened += OnSettingsChangedBehaviour;
        }

        private void Start() {
            LoadFromPlayerPrefs();
        }

        private void OnDestroy() {
            _settingsChangedListener.OnEventHappened -= OnSettingsChangedBehaviour;
        }

        private SavedDataWrapper GetWrapper() {
            var wrapper = new SavedDataWrapper {
                volume = _volumeAsset.value,
                difficult = _difficultAsset.value,
                night = _nightAsset.value
            };
            
            return wrapper;
        }

        public void SaveToPlayerPrefs() {
            var wrapper = GetWrapper();
            
            var json = JsonUtility.ToJson(wrapper);
            PlayerPrefs.SetString(SETTINGS_KEY, json);
        }
        
        private void LoadFromPlayerPrefs() {
            if (!PlayerPrefs.HasKey(SETTINGS_KEY)) {
                return;
            }

            var wrapper = JsonUtility.FromJson<SavedDataWrapper>(PlayerPrefs.GetString(SETTINGS_KEY));
            _volumeAsset.value = wrapper.volume;
            _difficultAsset.value = wrapper.difficult;
            _nightAsset.value = wrapper.night;
            
            _settingsLoaded.Dispatch();
        }

        private void OnSettingsChangedBehaviour() {
            SaveToPlayerPrefs();
        }
    }
}

