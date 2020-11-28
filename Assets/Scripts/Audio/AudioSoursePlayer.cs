using Sirenix.OdinInspector;
using UnityEngine;
using Game;

namespace Audio { 
public class AudioSoursePlayer : MonoBehaviour {

        [SerializeField]
        public AudioSource _audioSourse;

        [Button]
        public void Play() {
            _audioSourse.Play();
        }

        [Button]
        public void Stop() {
            _audioSourse.Stop();
        }
    }
}
