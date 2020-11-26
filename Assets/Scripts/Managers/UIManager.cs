using System.Collections;
using Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI {

    public class UIManager : MonoBehaviour {

        [SerializeField]
        private GameObject _menuScreen;

        [SerializeField]
        private GameObject _gameScreen;

        [SerializeField]
        private GameObject _leaderBoardScreen;

        [SerializeField]
        private GameObject _settingsScreen;

        [SerializeField]
        private Fader _fader;

        [SerializeField]
        private MusicManager _musicManager;

        public static UIManager instance;

        private void Awake() {
            if (instance != null) {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void OnEnable() {
            _fader.FadeIn();
            ShowMenuScreen();
            _musicManager.MusicFadeOut();
        }

        public void ShowMenuScreen() {
            HideAllScreens();
            _menuScreen.SetActive(true);
        }

        public void ShowGameScreen() {
            HideAllScreens();
            _gameScreen.SetActive(true);
        }

        public void ShowLeaderBoardScreen() {
            HideAllScreens();
            _leaderBoardScreen.SetActive(true);
        }

        public void ShowSettingsScreen() {
            _settingsScreen.SetActive(true);
        }

        public void CloseSettingsScreen() {
            _settingsScreen.SetActive(false);
        }

        public void HideAllScreens() {
            _menuScreen.SetActive(false);
            _gameScreen.SetActive(false);
            _leaderBoardScreen.SetActive(false);
            _settingsScreen.SetActive(false);
        }

        public void LoadMenu() {
            _musicManager.MusicFadeIn();
            _fader.OnFadeOut += LoadMenuScene;
            _fader.OnFadeOut += _musicManager.MusicFadeOut;
            _fader.FadeOut();
        }

        public void LoadGameplay() {
            _musicManager.MusicFadeIn();
            _fader.OnFadeOut += LoadGameplayScene;
            _fader.OnFadeOut += _musicManager.MusicFadeOut;
            _fader.FadeOut();
        }

        private IEnumerator FadeOutAndLoadGameplay() {
            yield return new WaitForSeconds(3f);

            _fader.OnFadeOut += LoadGameplayScene;
            _fader.FadeOut();
        }

        private void LoadMenuScene() {
            _fader.OnFadeOut -= LoadMenuScene;
            _fader.OnFadeOut -= _musicManager.MusicFadeOut;
            StartCoroutine(LoadSceneCoroutine("Menu"));
            ShowMenuScreen();
        }

        private void LoadGameplayScene() {
            _fader.OnFadeOut -= LoadGameplayScene;
            _fader.OnFadeOut -= _musicManager.MusicFadeOut;
            StartCoroutine(LoadSceneCoroutine("Gameplay"));
            ShowGameScreen();
        }

        private IEnumerator LoadSceneCoroutine(string sceneName) {
            _fader.FadeIn();
            var asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            while (!asyncOperation.isDone) {
                yield return null;
            }
        }

        public bool IsSettingsActive() {
            return _settingsScreen.activeInHierarchy;
        }
    }

}
