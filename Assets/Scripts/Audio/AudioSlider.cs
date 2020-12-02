using UnityEngine;
using UnityEngine.UI;

namespace Audio {

    [RequireComponent(typeof(Slider))]
    public class AudioSlider : MonoBehaviour {

        [SerializeField]
        private AudioManager _audioManager;

        [SerializeField]
        private Slider _slider;
        
        private void OnEnable() {
            _slider = GetComponent<Slider>();
            _slider.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnValueChanged(float value) {
            _audioManager.PlaySliderSound(value);
        }
    }
}
