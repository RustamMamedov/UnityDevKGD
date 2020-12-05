using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace UI {

    public class UICamera : MonoBehaviour {

        [SerializeField]
        private AudioListener _audioListener;

        public void set_enabled(float newVolume) {
            if (newVolume == 0f) {
                _audioListener.enabled = false;
                return;
            }
            _audioListener.enabled = true;
        }
    }
}