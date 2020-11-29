using System.Collections;
using System.Collections.Generic;
using Events;
using Game;
using UnityEngine;

namespace UI {

    public class MenusBackground : MonoBehaviour {

        [SerializeField]
        private EventListener _settingsGotSavedEventListener;

        [SerializeField] 
        private GameObject _dayLighting;
        
        [SerializeField] 
        private GameObject _nightLighting;

        [SerializeField] 
        private ScriptableBoolValue _isNight;

        private void OnEnable() {
            SetMenusBackgroundLighting();
            _settingsGotSavedEventListener.OnEventHappened += SetMenusBackgroundLighting;
        }
        
        private void OnDisable() {
            _settingsGotSavedEventListener.OnEventHappened -= SetMenusBackgroundLighting;
        }

        private void SetMenusBackgroundLighting() {
            _dayLighting.SetActive(!_isNight.value);
            _nightLighting.SetActive(_isNight.value);
        }
    }
}