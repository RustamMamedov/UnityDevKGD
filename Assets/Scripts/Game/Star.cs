using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;

namespace Game {

    public class Star : MonoBehaviour {

        [SerializeField]
        private GameObject _star;
        [SerializeField]
        private AudioSourcePlayer _takeAudio;
        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                _star.SetActive(false);
                _takeAudio.Play();
            }
        }
    }
}
