using UnityEngine;

namespace Audio {
    public class AudioManager : MonoBehaviour {
     
        [SerializeField]
        private AudioSourcePlayer _buttonSound;

        [SerializeField]
        private AudioSourcePlayer _sliderSound;

        public void PlayButtonSound() {
            _buttonSound.Play();
        }

        public void PlaySliderSound(float value) {
            _sliderSound.SetPitch(value / 2 + 0.5f);
            _sliderSound.Play();
        }
    }
}
