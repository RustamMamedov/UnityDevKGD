using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio {

    public class MusicManager : MonoBehaviour {

        [SerializeField]
        private AudioSourcePlayer _menuAudioSourecePlayer;

        [SerializeField]
        private AudioSourcePlayer _GamplayAudioSourecePlayer;

        [SerializeField]
        private float _timeAudio;

        public void OnMenuPlayMusic() {
            _menuAudioSourecePlayer.Play();
            StartCoroutine(_menuAudioSourecePlayer.PlayGradually(_timeAudio));
        }

        public void OnGamplayPlayMusic() {
            _GamplayAudioSourecePlayer.Play();
            StartCoroutine(_GamplayAudioSourecePlayer.PlayGradually(_timeAudio));
        }

        public void OnStopedMennuMusic() {
            StartCoroutine(_menuAudioSourecePlayer.StopGradually(_timeAudio));
        }
        public void OnStopedGameplayMusic() {
            StartCoroutine(_GamplayAudioSourecePlayer.StopGradually(_timeAudio));
        }
    }
}
