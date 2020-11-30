using Events;
using Sirenix.OdinInspector;
using UnityEngine;
using Values;

namespace Audio {

    public class AudioSourcePlayer : MonoBehaviour {

        // Fields.

        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private ScriptableFloatValue _volumeFactorValue;

        // Currently set volume, before factors.
        private float _currentVolume = 0;

        // Volume factor from audio source component.
        private float _playVolume;

        // When not-null, volume is gradually changing to the value.
        private float? _targetVolume = null;

        // Volume change to target, per second.
        private float _volumeChangeSpeed;


        // Life cycle.

        private void Start() {
            _playVolume = _audioSource.volume;
            _audioSource.volume = 0;
        }

        private void LateUpdate() {

            // Move volume torwards target.
            if (_targetVolume.HasValue) {
                _currentVolume = Mathf.MoveTowards(_currentVolume, _targetVolume.Value, _volumeChangeSpeed * Time.deltaTime);
                if (_currentVolume == _targetVolume.Value) {
                    _targetVolume = null;
                }
            }

            // Set properties of audio source.
            if (_audioSource.volume > 0 && !_audioSource.isPlaying) {
                SetVolume(0);
            }
            _audioSource.volume = _currentVolume * _playVolume * _volumeFactorValue.value;
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


        // Methods.

        [Button]
        public void Play() => SetVolume(1);

        [Button]
        public void Stop() => SetVolume(0);

        public void Play(float changeDuration) => SetVolume(1, changeDuration);

        public void Stop(float changeDuration) => SetVolume(0, changeDuration);


        // Volume changing methods.

        private void SetVolume(float newVolume) {
            _currentVolume = newVolume;
            _targetVolume = null;
        }

        private void SetVolume(float newVolume, float changeDuration) {
            _volumeChangeSpeed = Mathf.Abs(_currentVolume - newVolume) / changeDuration;
            _targetVolume = newVolume;
        }


    }

}
