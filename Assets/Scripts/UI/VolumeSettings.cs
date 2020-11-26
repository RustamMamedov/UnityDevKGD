using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class VolumeSettings : MonoBehaviour {

        [SerializeField]
        private Text _volumeValueText;

        [SerializeField]
        private Slider _volumeSlider;

        private void Start() {
            _volumeSlider.onValueChanged.AddListener(delegate { OnVolumeChanged(); });
        }

        private void OnVolumeChanged() {
            _volumeValueText.text = Mathf.Round((_volumeSlider.value * 100)).ToString() + "%";
        }
    }
}
