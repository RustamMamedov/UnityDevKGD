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
        private GameObject _settingsScreen;

        [SerializeField]
        private MusicManager _musicManager;

        [SerializeField]
        private SettingsScreen _save;

        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start() {
            _save.LoadFromPlayerPrefs();
            ShowMenuScreen(_save.getVolume());
        }

        public void LoadMenu() {
            _fader.OnFadeOut += LoadMenuScene;
            _fader.FadeOut();
        }

        public void LoadGameplay() {
            _musicManager.StopMenuMusic();
            _fader.OnFadeOut += LoadGameplayScene;
            _fader.FadeOut();
        }

        private void LoadMenuScene() {
            _fader.OnFadeOut -= LoadMenuScene;
            StartCoroutine(LoadSceneCoroutine("Menu"));
            ShowMenuScreen(_save.getVolume());
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

        public void ShowMenuScreen(float maxVolume) {
            HideAllScreens();
            _menuScreen.SetActive(true);
            _musicManager.PlayMenuMusic(maxVolume);

        }

        public void ShowGameScreen() {
            HideAllScreens();
            _gameScreen.SetActive(true);
            _musicManager.PlayGameMusic();
        }

        public void ShowLeaderboardsScreen() {
            HideAllScreens();
            _leaderboardScreen.SetActive(true);
        }

        public void ShowSettingsScreen() {
            HideAllScreens();
            _settingsScreen.SetActive(true);
        }

        public void HideAllScreens() {

            if (_menuScreen.activeSelf == true) {
                _menuScreen.SetActive(false);
            }
            if (_gameScreen.activeSelf == true) {
                _musicManager.StopGameMusic();
                _gameScreen.SetActive(false);
            }
            if (_leaderboardScreen.activeSelf == true) {
                _leaderboardScreen.SetActive(false);
            }
            if (_settingsScreen.activeSelf == true) {
                _settingsScreen.SetActive(false);
            }
        }

    }
}
