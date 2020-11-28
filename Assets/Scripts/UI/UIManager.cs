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
        private AudioSourcePlayer _menuMusic;

        [SerializeField]
        private AudioSourcePlayer _gameMusic;

        [SerializeField]
        private GameObject _leaderboard;

        [SerializeField]
        private GameObject _menuScreen;

        [SerializeField]
        public GameObject GameScreen;

        [SerializeField]
        private MusicManager _musicManager;

        // private string _currentSceneName = "Gameplay";

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

        public void LoadGamePlay() {
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
            _menuScreen.SetActive(true);
            _musicManager.PlayMenuMusic();
        }

        public void ShowGameScreen() {
            HideAllScreens();
            GameScreen.SetActive(true);
            _gameMusic.Play();            
        }

        public void ShowLeaderboardsScreen() {
            HideAllScreens();
            _leaderboard.SetActive(true);
        }

        public void HideAllScreens() {
            _menuScreen.SetActive(false);
            GameScreen.SetActive(false);
            _leaderboard.SetActive(false);
        }
    }
}