using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio {

    public class OnCollisionAudio : MonoBehaviour {

        [SerializeField]
        private AudioSourcePlayer _audioSourcePlayer;

        public void PlayAudio() {
            _audioSourcePlayer.Play();
        }

    }
}