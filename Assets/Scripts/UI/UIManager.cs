using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using Audio;

namespace UI {

    public class UIManager : MonoBehaviour {

        public static UIManager instance;

        [SerializeField]
        private GameObject _menuScreen;

        [SerializeField]
        private GameObject _gameScreen;

        [SerializeField]
        private GameObject _leaderboardScreen;

        [SerializeField]
        private MusicManager _musicManager;

        [SerializeField]
        private Fader _fader;

        private void Awake() {
            if(instance != null) {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start() {
            ShowMenuScreen();
        }

        public void LoadMenu() {
            _fader.OnFadeOut += LoadMenuScene;
            _fader.FadeOut();
        }

        public void LoadGamePlay() {
            _fader.OnFadeOut += LoadGamePlayScene;
            _fader.FadeOut();
        }

        private void LoadGamePlayScene() {
            _fader.OnFadeOut -= LoadGamePlayScene;
            StartCoroutine(LoadSceneCoroutine("GamePlay"));
            ShowGameScreen();
        }
        private void LoadMenuScene() {
            _fader.OnFadeOut -= LoadGamePlayScene;
            StartCoroutine(LoadSceneCoroutine("Menu"));
            ShowMenuScreen();
        }

        private IEnumerator LoadSceneCoroutine(string scenename) {
            var asyncOp = SceneManager.LoadSceneAsync(scenename);
            while (!asyncOp.isDone) {
                yield return null;
            }
            _fader.FadeIn();
        }

        private void ShowMenuScreen() {
            HideAllScreens();
            _menuScreen.SetActive(true);
            _musicManager.PlayMenuMusic();
        }

        private void ShowGameScreen() {
            HideAllScreens();
            _gameScreen.SetActive(true);
        }

        public void ShowLeaderboardScreen() {
            HideAllScreens();
            _leaderboardScreen.SetActive(true);
        }

        private void HideAllScreens() {
            _menuScreen.SetActive(false);
            _gameScreen.SetActive(false);
            _leaderboardScreen.SetActive(false);
        }
    }
}

