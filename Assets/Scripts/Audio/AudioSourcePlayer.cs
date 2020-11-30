using Sirenix.OdinInspector;
using UnityEngine;
using Game;

namespace Audio {

    public class AudioSourcePlayer : MonoBehaviour {

        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private ScriptableFloatValue _volume;


        [Button]
        public void Play() {
            _audioSource.volume = _volume.value;
            _audioSource.Play();
            Debug.Log("play");
        }

        [Button]
        public void Stop() {
            _audioSource.Stop();
        }
    }
}