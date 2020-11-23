using Sirenix.OdinInspector;
using UnityEngine;

namespace Audio {

    public class AudioSourcePlayer : MonoBehaviour {

        // Fields.

        [SerializeField]
        private AudioSource _audioSource;


        // Methods.

        [Button]
        public void Play() {
            _audioSource.Play();
        }

        [Button]
        public void Stop() {
            _audioSource.Stop();
        }


    }

}
