using System.Collections;
using UnityEngine.Audio;
using UnityEngine;
using Events;
using Game;

namespace Audio {

    public class MusicManager : MonoBehaviour {

        public static MusicManager Instance;

        [SerializeField]
        public AudioMixer _master;

        [SerializeField]
        private AudioSourcePlayer _menuMusicPlayer;

        [SerializeField]
        private AudioSourcePlayer _gameMusicPlayer;

        [SerializeField]
        private AudioSourcePlayer _carDodgeSoundPlayer;

        [SerializeField]
        private AudioSourcePlayer _carCollisionSoundPlayer;

        [SerializeField]
        private EventListener _carDodgeEventListener;

        [SerializeField]
        private EventListener _carCollisionEventListener;

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private ScriptableFloatValue _volume;

        [SerializeField]
        private float _fadeTime = .3f;

        private AudioSourcePlayer _currentPlayer;

        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            _carDodgeEventListener.OnEventHappened += OnCarDodge;
            _carCollisionEventListener.OnEventHappened += OnCarCollision;
            _updateEventListener.OnEventHappened += OnUpdate;

            _currentPlayer = _menuMusicPlayer;
            _menuMusicPlayer.Play();
        }

        private void OnCarDodge() {
            _carDodgeSoundPlayer.Play();
        }

        private void OnCarCollision() {
            _carCollisionSoundPlayer.Play();
        }

        private void OnUpdate() {
            SetVolume(_volume.value);
        }

        public void PlayMenuMusic() {
            if (_menuMusicPlayer.IsPlaying) {
                return;
            }
            StartCoroutine(FadeMusicTo(_menuMusicPlayer));
        }

        public void PlayGameMusic() {
            if (_gameMusicPlayer.IsPlaying) {
                return;
            }
            StartCoroutine(FadeMusicTo(_gameMusicPlayer));
        }

        private IEnumerator FadeMusicTo(AudioSourcePlayer to) {
            if (_currentPlayer == null) {
                yield return StartCoroutine(FadeMusicIn(to));
                yield break;
            }

            yield return StartCoroutine(FadeMusicOut(_currentPlayer));
            StartCoroutine(FadeMusicIn(to));
        }

        private IEnumerator FadeMusicIn(AudioSourcePlayer to) {
            if (!to.IsPlaying) {
                to.Play();
            }
            if (_currentPlayer == to) {
                yield break;
            }
            _currentPlayer = to;

            var timer = 0f;
            var halfFadeTime = _fadeTime / 2f;
            while (timer < halfFadeTime) {
                timer += Time.deltaTime;
                var volume = Mathf.Lerp(0f, 1f, timer / halfFadeTime);
                to.SetVolume(volume);
                yield return null;
            }
        }

        private IEnumerator FadeMusicOut(AudioSourcePlayer to) {
            var timer = 0f;
            var halfFadeTime = _fadeTime / 2f;
            while (timer < halfFadeTime) {
                timer += Time.deltaTime;
                var volume = Mathf.Lerp(1f, 0f, timer / halfFadeTime);
                to.SetVolume(volume);
                yield return null;
            }
            to.Stop();
        }

        public void SetVolume(float value) {
            _master.SetFloat("master", Mathf.Log10(value) * 20);
        }
    }
}