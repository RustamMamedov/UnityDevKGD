using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game;


namespace Audio {
    public class Volume : MonoBehaviour {

        [SerializeField]
        Slider _sliderVolume;

        [SerializeField]
        private ScriptableFloatValue _volumeMusic;

        private void Update() {

            _volumeMusic.value = _sliderVolume.value;
            AudioListener.volume = _volumeMusic.value;
        }
    }
}