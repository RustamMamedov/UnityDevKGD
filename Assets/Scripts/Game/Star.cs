using UnityEngine;
using Audio;

namespace Game {

    public class Star : MonoBehaviour {

        [SerializeField]
        private AudioSourcePlayer _starAudio;

        private void OnTriggerEnter(Collider other) {
            if (other.tag == "Player") {
                _starAudio.Play();
            }
        }
    }
}