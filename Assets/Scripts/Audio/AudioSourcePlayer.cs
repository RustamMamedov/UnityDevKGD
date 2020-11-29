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
        private EventListener _savedSettingsEventListener;
        private void OnEnable() {
            ChangeVolume();
            _savedSettingsEventListener.OnEventHappened += ChangeVolume;
        }

        private void OnDisable() {
            _savedSettingsEventListener.OnEventHappened -= ChangeVolume;
        }

        private void ChangeVolume() {
            _audioSource.volume = PlayerPrefs.GetFloat(DataKeys.VOLUME_KEY);
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
            StartCoroutine(CoroutineMusicVolume(0f, PlayerPrefs.GetFloat(DataKeys.VOLUME_KEY), requiredTime));
        }

        public IEnumerator StopGradually(float requiredTime) {
            yield return StartCoroutine(CoroutineMusicVolume(PlayerPrefs.GetFloat(DataKeys.VOLUME_KEY), 0f, requiredTime));
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
