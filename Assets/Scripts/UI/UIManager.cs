using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI {

    public class UIManager : MonoBehaviour {

        public static UIManager Instance;

        [SerializeField]
        private Fader _fader;

        [SerializeField]
        private ScoreView _scoreView;

        [SerializeField]
        private GameObject _menuScreen;

        [SerializeField]
        private GameObject _gameScreen;

        [SerializeField]
        private GameObject _leaderboardsScreen;

        private string _currentSceneName = "Gameplay";

        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            //ShowMenuScreen(); // проверка на работоспособность
            //ShowLeaderboardsScreen();
            //ShowGameScreen();
            //HideAllScreens();
        }

        private void Start() { }

        private void OnSceneFadeIn() {
            StartCoroutine(FadeOutAndLoadGameplay());
        }

        private IEnumerator FadeOutAndLoadGameplay() {
            yield return new WaitForSeconds(3f);

            _fader.OnFadeOut += LoadGameplayScene;
            _fader.FadeOut();
        }

        public void LoadMenu(){
            _fader.OnFadeOut += LoadMenuScene;
            _fader.FadeOut();
        }

        private void LoadMenuScene() {
            _fader.OnFadeOut -= LoadMenuScene;
            StartCoroutine(LoadSceneCoroutine("Menu"));
            ShowMenuScreen();
        }

        public void LoadGameplay(){
            _fader.OnFadeOut += LoadGameplayScene;
            _fader.FadeOut();
            ShowGameScreen();
        }

        private void LoadGameplayScene() {
            _fader.OnFadeOut -= LoadGameplayScene;
            StartCoroutine(LoadSceneCoroutine("Gameplay"));
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
        }
        public void ShowGameScreen() {
            HideAllScreens();
            _gameScreen.SetActive(true);
        }
        public void ShowLeaderboardsScreen() {
            HideAllScreens();
            _leaderboardsScreen.SetActive(true);
        }

        public void HideAllScreens() {
            _menuScreen.SetActive(false);
            _gameScreen.SetActive(false);
            _leaderboardsScreen.SetActive(false);
        }
    }
}