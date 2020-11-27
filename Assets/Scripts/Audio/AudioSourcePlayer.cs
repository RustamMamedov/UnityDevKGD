using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Audio {

    public class AudioSourcePlayer : MonoBehaviour {

        [SerializeField]
        public AudioSource audioSource;

        [Button]
        public void Play() {
            audioSource.Play();
        }

        [Button]
        public void Stop() {
            audioSource.Stop();
        }
    }
}