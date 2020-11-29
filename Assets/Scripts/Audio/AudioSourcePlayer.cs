using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Game;

namespace Audio {

    public class AudioSourcePlayer : MonoBehaviour {

        [SerializeField]
        public AudioSource audioSource;

        [SerializeField]
        private ScriptableFloatValue _soundVolume;

        private void Start() {
            _soundVolume.value = PlayerPrefs.GetFloat("MusicVolume");
        }

        [Button]
        public void Play() {
            audioSource.Play();
        }

        [Button]
        public void Stop() {
            audioSource.Stop();
        }

        private void Update() {
            audioSource.volume = _soundVolume.value;
        }


    }
}

