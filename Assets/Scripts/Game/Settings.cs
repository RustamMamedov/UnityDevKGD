using System;
using UnityEngine;

namespace Game {
    public class Settings : MonoBehaviour {

        private static float _volume = 0.5f;
        private static bool _isDifficult = false;
        private static bool _isNight = false;
        
        public static float Volume {
            get => _volume;
            set => _volume = value;
        }

        public static bool IsDifficult {
            get => _isDifficult;
            set => _isDifficult = value;
        }

        public static bool IsNight {
            get => _isNight;
            set => _isNight = value;
        }

        private class SavedDataWrapper {

            public float volume;
            public bool difficult;
            public bool night;
        }
        
        private const string SETTINGS_KEY = "Settings";

        private void Awake() {
            LoadFromPlayerPrefs();
        }

        private static SavedDataWrapper GetWrapper() {
            var wrapper = new SavedDataWrapper {
                volume = _volume,
                difficult = _isDifficult,
                night = _isNight
            };
            
            return wrapper;
        }

        public static void SaveToPlayerPrefs() {
            var wrapper = GetWrapper();
            
            var json = JsonUtility.ToJson(wrapper);
            PlayerPrefs.SetString(SETTINGS_KEY, json);
            
            Debug.Log("Json: " + json);
        }
        
        private void LoadFromPlayerPrefs() {
            if (!PlayerPrefs.HasKey(SETTINGS_KEY)) {
                return;
            }

            var wrapper = JsonUtility.FromJson<SavedDataWrapper>(PlayerPrefs.GetString(SETTINGS_KEY));
            _volume = wrapper.volume;
            _isDifficult = wrapper.difficult;
            _isNight = wrapper.night;
        }
    }
}

