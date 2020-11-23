using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio {

    public class DodgeAudio : MonoBehaviour {
        
        [SerializeField]
        private AudioSourcePlayer _audioSourcePlayer;

        public void PlayAudio() {
            _audioSourcePlayer.Play();
        }
    }

}