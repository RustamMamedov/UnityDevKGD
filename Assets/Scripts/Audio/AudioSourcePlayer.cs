using Sirenix.OdinInspector;
using UnityEngine;

namespace Audio {

    public class AudioSourcePlayer : MonoBehaviour {

        // Fields.

        [SerializeField]
        private AudioSource _audioSource;

        // Volume that set on play.
        private float _defaultVolume;

        // When not-null, volume is gradually changing to the value.
        private float? _targetVolume;

        // Volume change to target, per second.
        private float _volumeChangeSpeed;


        // Life cycle.

        private void Awake() {
            _defaultVolume = _audioSource.volume;
            _audioSource.volume = 0;
        }

        private void Update() {
            UpdateVolume();
        }


        // Methods.

        [Button]
        public void Play() => SetVolume(_defaultVolume);

        [Button]
        public void Stop() => SetVolume(0);

        public void Play(float changeDuration) => SetVolume(_defaultVolume, changeDuration);

        public void Stop(float changeDuration) => SetVolume(0, changeDuration);


        // Volume changing methods.

        private void SetVolume(float newVolume) {
            _targetVolume = null;
            UpdateAudioSource(newVolume);
        }

        private void SetVolume(float newVolume, float changeDuration) {
            _volumeChangeSpeed = _defaultVolume / changeDuration;
            _targetVolume = newVolume;
        }

        private void UpdateVolume() {
            if (_targetVolume.HasValue) {
                var newVolume = Mathf.MoveTowards(_audioSource.volume, _targetVolume.Value, _volumeChangeSpeed * Time.deltaTime);
                if (newVolume == _targetVolume.Value) {
                    _targetVolume = null;
                }
                UpdateAudioSource(newVolume);
            }
        }

        private void UpdateAudioSource(float newVolume) {
            _audioSource.volume = newVolume;
            if (_audioSource.volume == 0) {
                if (_audioSource.isPlaying) {
                    _audioSource.Stop();
                }
            } else {
                if (!_audioSource.isPlaying) {
                    _audioSource.Play();
                }
            }
        }


    }

}
