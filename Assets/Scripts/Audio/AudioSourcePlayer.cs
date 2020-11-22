using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Audio {

    public class AudioSourcePlayer : MonoBehaviour {

        [SerializeField]
        private AudioSource _audioSourece;

        [Button]
        public void Play(){
            _audioSourece.Play();
        }

        [Button]
        public void Stop() {
            _audioSourece.Stop();
        }
    }
}
