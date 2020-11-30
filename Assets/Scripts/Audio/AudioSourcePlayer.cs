using Sirenix.OdinInspector;
using UnityEngine;
using System.Collections;
using Game;
using Events;
namespace Audio {
    public class AudioSourcePlayer : MonoBehaviour {

        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private ScriptableFloatValue _volume;

        [SerializeField]
        private EventListener _changeEventListener;
        private void OnEnable() {
            ChangeVolume();
            _changeEventListener.OnEventHappened += ChangeVolume;
        }

        private void OnDisable() {
            _changeEventListener.OnEventHappened -= ChangeVolume;
        }

        private void ChangeVolume() {
            _audioSource.volume = _volume.value;
        }

        [Button]
        public void Play() {
            _audioSource.Play();
        }

        [Button]
        public void Stop() {
            _audioSource.Stop();
        }

        public void PlayMusic(float requiredTime) {
            _audioSource.volume = 0;
            Play();
            StartCoroutine(CoroutineMusicVolume(0f, _volume.value, requiredTime));
        }

        public IEnumerator StopGradually(float requiredTime) {
            yield return StartCoroutine(CoroutineMusicVolume(_volume.value, 0f, requiredTime));
            Stop();
        }

        private IEnumerator CoroutineMusicVolume(float from, float to, float requiredTime) {
            var time = 0f;
            _audioSource.volume = from;
            while (time < requiredTime) {
                time += Time.deltaTime;
                _audioSource.volume = Mathf.Lerp(_audioSource.volume, to, time / requiredTime);
                yield return null;
            }
            
        }

    }
}
