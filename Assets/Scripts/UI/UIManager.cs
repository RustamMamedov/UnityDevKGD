using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        private string _currentSceneName = "Gameplay";

        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start() {
        }

        private void OnSceneFadeIn() {
            StartCoroutine(FadeOutAndLoadGameplay());
        }

        private IEnumerator FadeOutAndLoadGameplay() {
            yield return new WaitForSeconds(3f);

            _fader.OnFadeOut += LoadGameplayScene;
            _fader.FadeOut();
        }

        private void LoadGameplayScene() {
            _fader.OnFadeOut -= LoadGameplayScene;
            StartCoroutine(LoadSceneCoroutine(_currentSceneName));
            _currentSceneName = _currentSceneName == "Gameplay" ? "Menu" : "Gameplay";
        }

        private IEnumerator LoadSceneCoroutine(string sceneName) {
            var asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            while (!asyncOperation.isDone) {
                yield return null;
            }

            yield return new WaitForSeconds(3f);

            _fader.FadeIn();
        }
        public void ShowMenuScreen() {
            HideAllScreens();
            if (_menuScreen.activeSelf == false) {
                _menuScreen.SetActive(true);
            }
        }

        public void ShowGameScreen() {
            HideAllScreens();
            if (_gameScreen.activeSelf == false) {
                _gameScreen.SetActive(true);
            }
        }

        public void ShowLeaderboardsScreen() {
            HideAllScreens();
            if (_leaderboardScreen.activeSelf == false) {
                _leaderboardScreen.SetActive(true);
            }
        }

        public void HideAllScreens() {

            if (_menuScreen.activeSelf == true) {
                _menuScreen.SetActive(false);
            }
            if (_gameScreen.activeSelf == true) {
                _gameScreen.SetActive(false);
            }
            if (_leaderboardScreen.activeSelf == true) {
                _leaderboardScreen.SetActive(false);
            }
        }

    }
}
