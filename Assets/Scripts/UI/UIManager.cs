using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

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
            _fader.OnFadeIn += OnSceneFadeIn;
            _fader.FadeIn();
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
            if (!_menuScreen.IsActive) {
                _menuScreen.SetActive(true);
            }
        }

        public void ShowGameScreen() {
            HideAllScreens();
            if (!_gameScreen.IsActive) {
                _gameScreen.SetActive(true);
            }
        }

        public void ShowLeaderboardsScreen() {
            HideAllScreens();
            if (!_leaderboardScreen.IsActive) {
                _leaderboardScreen.SetActive(true);
            }
        }

        public void HideAllScreens() {

            if (_menuScreen.IsActive) {
                _menuScreen.SetActive(false);
            }
            if (_gameScreen.IsActive) {
                _gameScreen.SetActive(false);
            }
            if (_leaderboardScreen.IsActive) {
                _leaderboardScreen.SetActive(false);
            }
        }
}