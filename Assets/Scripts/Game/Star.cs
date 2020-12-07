using Events;
using Audio;
using UnityEngine;

namespace Game {

    public class Star : MonoBehaviour {

        [SerializeField]
        private EventDispatcher _starCollectedEventDispatcher;

        [SerializeField]
        private AudioSourcePlayer _starAudio;

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                _starCollectedEventDispatcher.Dispatch();
                _starAudio.Play();
            }
        }
    }
}