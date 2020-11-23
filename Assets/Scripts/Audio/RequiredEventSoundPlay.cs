using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

namespace Audio {

    public class RequiredEventSoundPlay : MonoBehaviour {

        [SerializeField]
        private EventListener _eventListener;

        [SerializeField]
        private AudioSourcePlayer _audioSource;

        private void OnEnable() {
            _eventListener.OnEventHappened += _audioSource.Play;
        }

        private void OnDisable() {
            _eventListener.OnEventHappened -= _audioSource.Play;
        }
    }
}
