using System.Collections;
using System.Collections.Generic;
using Events;
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
        private bool _isNight;

        private void Awake() {
            SetMenusBackgroundLighting();
        }
        
        private void OnEnable() {
            _settingsGotSavedEventListener.OnEventHappened += SetMenusBackgroundLighting;
        }
        
        private void OnDisable() {
            _settingsGotSavedEventListener.OnEventHappened -= SetMenusBackgroundLighting;
        }

        private void SetMenusBackgroundLighting() {
            _dayLighting.SetActive(!_isNight);
            _nightLighting.SetActive(_isNight);
        }
    }
}