using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;
using Game;

namespace Audio {

    public class AudioSourcePlayer : MonoBehaviour {

        [SerializeField]
        private AudioSource _audioSourece;

        [SerializeField]
        [Range(0f, 1f)]
        private float _minVolume = 0f;

        [SerializeField]
        private ScriptableFloatValue _maxVolume;

        [Button]
        public void Play() {
            _audioSourece.Play();
        }

        [Button]
        public void Stop() {
            _audioSourece.Stop();
        }

        public void SetVolume() {
            _audioSourece.volume=_maxVolume.Value; 
        }

        public void SetVolume(float volume) {
            if (_audioSourece.isPlaying) _audioSourece.volume = volume;
            _maxVolume.Value = volume;
        }

        public IEnumerator PlayGradually(float timeAudio) {
            Play();
            yield return StartCoroutine(CoroutineMusicVolume(_minVolume, _maxVolume.Value, timeAudio));
        }

        public IEnumerator StopGradually(float timeAudio) {
            yield return StartCoroutine(CoroutineMusicVolume(_maxVolume.Value, _minVolume, timeAudio));
            Stop();
        }

        private IEnumerator CoroutineMusicVolume(float fromAlfa,float targetAlfa, float timeAudio) {
            _audioSourece.volume = fromAlfa;
            var time = 0f;
            while (time < timeAudio) {
                time += Time.deltaTime;
                _audioSourece.volume = Mathf.Lerp(_audioSourece.volume, targetAlfa, time/ timeAudio);
                yield return null;
            }
        }
    }
}
