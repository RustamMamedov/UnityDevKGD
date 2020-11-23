using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Audio;

namespace UI {

    public class UIManager : MonoBehaviour {

        public static UIManager Instance;

        [SerializeField]
        private Fader _fader;

        [SerializeField]
        private GameObject _menuScreen;

        [SerializeField]
        private GameObject _gameScreen;

        [SerializeField]
        private GameObject _leaderboardScreen;

        [SerializeField]
        private MusicManager _musicManager;
        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        private void Start() {
            ShowMenuScreen();
        }
        public void LoadMenu() {
            _fader.OnFadeOut += LoadMenuScene;
            _fader.FadeOut();
        }

        public void LoadGameplay() {
            _fader.OnFadeOut += LoadGameplayScene;
            _fader.FadeOut();
        }
        private void LoadMenuScene() {
            _fader.OnFadeOut -= LoadMenuScene;
            StartCoroutine(LoadSceneCoroutine("Menu"));
            ShowMenuScreen();
        }
        private void LoadGameplayScene() {
            _fader.OnFadeOut -= LoadGameplayScene;
            StartCoroutine(LoadSceneCoroutine("Gameplay"));
            ShowGameScreen();
        }
        private IEnumerator LoadSceneCoroutine(string sceneName) {
            var asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            while (!asyncOperation.isDone) {
                yield return null;
            }
            _fader.FadeIn();
        }

        public void ShowMenuScreen() {
            HideAllScreens();
            StartCoroutine(_musicManager.MuteGameCoroutine());
            _menuScreen.SetActive(true);
            _musicManager.PlayMenuMusic();
        }

        public void ShowGameScreen() {
            HideAllScreens();
            StartCoroutine(_musicManager.MuteMenuCoroutine());
            _gameScreen.SetActive(true);
            _musicManager.PlayGameMusic();
        }

        public void ShowLeaderboardsScreen() {
            HideAllScreens();
            _leaderboardScreen.SetActive(true);
        }

        public void HideAllScreens() {
            _menuScreen.SetActive(false);
            _gameScreen.SetActive(false);
            _leaderboardScreen.SetActive(false);
        }
    }
}