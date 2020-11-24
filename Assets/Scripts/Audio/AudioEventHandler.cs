using Events;
using UnityEngine;

namespace Audio {
    
    public class AudioEventHandler : MonoBehaviour {
    
        // Fields.

        [SerializeField]
        private EventListener _eventListener;

        [SerializeField]
        private AudioSourcePlayer _audioSourcePlayer;


        // Life cycle.

        private void OnEnable() {
            _eventListener.OnEventHappened += PlaySound;
        }

        private void OnDisable() {
            _eventListener.OnEventHappened -= PlaySound;
        }


        // Event handling.

        private void PlaySound() {
            _audioSourcePlayer.Play();
        }


    }

}
