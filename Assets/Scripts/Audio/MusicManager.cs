using UnityEngine;
using Events;
using Game;
using UI;


namespace Audio {

    public class MusicManager : MonoBehaviour {

        [SerializeField]
        private AudioSourcePlayer _menuAudioSourecePlayer;

        [SerializeField]
        private AudioSourcePlayer _gamplayAudioSourecePlayer;

        [SerializeField]
        private EventListeners _updateEventListener;

        [SerializeField]
        private float _timeAudio;

        [SerializeField]
        private ScriptableFloatValue _valueSound;

        private void SetVolumes() {
            if (UIManager.Instance.GetActivScreenSettings()) {
                _menuAudioSourecePlayer.SetVolume(_valueSound.Value);
                _gamplayAudioSourecePlayer.SetVolume(_valueSound.Value);
            }
        }

        private void OnEnable() {
            _updateEventListener.OnEventHappened += SetVolumes;
        }

        private void OnDisable() {
            _updateEventListener.OnEventHappened -= SetVolumes;
        }

        public void OnMenuPlayMusic() {
            _menuAudioSourecePlayer.Play();
            StartCoroutine(_menuAudioSourecePlayer.PlayGradually(_timeAudio));
        }

        public void OnGamplayPlayMusic() {
            _gamplayAudioSourecePlayer.Play();
            StartCoroutine(_gamplayAudioSourecePlayer.PlayGradually(_timeAudio));
        }

        public void OnStopedMennuMusic() {
            StartCoroutine(_menuAudioSourecePlayer.StopGradually(_timeAudio));
        }
        public void OnStopedGameplayMusic() {
            StartCoroutine(_gamplayAudioSourecePlayer.StopGradually(_timeAudio));
        }
    }
}
