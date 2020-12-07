using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;

namespace Game {

    public class Stars : MonoBehaviour {
        
        [SerializeField]
        private GameObject _star;
        
        [SerializeField]
        private AudioSourcePlayer _takeAudio;
        
        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                _takeAudio.Play();
                _star.SetActive(false);
            }
        }
    }
}