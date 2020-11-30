using Sirenix.OdinInspector;
using UnityEngine;
using Events;
using Game;

namespace Audio {

    public class AudioSourcePlayer : MonoBehaviour {

        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private EventListener _update;

        [SerializeField]
        private ScriptableFloatValue _volumeCurrent;

        [Button]
        public void Play() {
            _audioSource.Play();
            Debug.Log("play");
        }


        private void OnEnable() {
            _update.OnEventHappened += UpdateBehaviour;
        }

        private void UpdateBehaviour() {
            MixerSound();
        }

        private void MixerSound() {
            _audioSource.volume = _volumeCurrent.value;
        }

        [Button]
        public void Stop() {
            _audioSource.Stop();
        }
    }
}