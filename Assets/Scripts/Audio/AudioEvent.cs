using Events;
using UnityEngine;

namespace Audio {
    
    [RequireComponent(typeof(EventListener))]
    public class AudioEvent : MonoBehaviour {

        [SerializeField] 
        private AudioSourcePlayer _audioSourcePlayer;
        
        private EventListener _eventListener;

        private void Awake() {
            _eventListener = GetComponent<EventListener>();
            _eventListener.OnEventHappened += OnEventBehaviour;
        }
        
        private void OnDestroy() {
            _audioSourcePlayer.Stop();
            _eventListener.OnEventHappened -= OnEventBehaviour;
        }

        private void OnEventBehaviour() {
            _audioSourcePlayer.Play();
        }
    }
}