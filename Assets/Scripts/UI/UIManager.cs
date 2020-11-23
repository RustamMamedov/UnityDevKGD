using Audio;
using Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace UI {

    public class UIManager : GameSingletonBase<UIManager> {

        // Constants.

        private const string _menuSceneName = "Menu";
        private const string _gameplaySceneName = "Gameplay";


        // Fields.

        [SerializeField]
        private Fader _fader;

        [SerializeField]
        private GameObject _menuScreen;

        [SerializeField]
        private GameObject _gameScreen;

        [SerializeField]
        private GameObject _leaderboardScreen;

        [SerializeField]
        private EventListener _progressSavedEventListener;

        [SerializeField]
        private MusicManager _musicManager;

        private string _loadingSceneName = null;


        // Life cycle.

        protected override void Awake() {
            base.Awake();
            _progressSavedEventListener.OnEventHappened += ShowLeaderboardScreen;
        }

        private void Start() {
            PrepareScene();
        }

        protected override void OnDestroy() {
            _progressSavedEventListener.OnEventHappened -= ShowLeaderboardScreen;
            base.OnDestroy();
        }


        // Scene loading.

        public void LoadGameplayScene() {
            if (_loadingSceneName != null) {
                return;
            }
            _loadingSceneName = _gameplaySceneName;
            _fader.OnFadeOut += ContinueSceneLoading;
            _fader.FadeOut();
        }

        public void LoadMenuScene() {
            if (_loadingSceneName != null) {
                return;
            }
            _loadingSceneName = _menuSceneName;
            _fader.OnFadeOut += ContinueSceneLoading;
            _fader.FadeOut();
        }

        private void ContinueSceneLoading() {
            _fader.OnFadeOut -= ContinueSceneLoading;
            StartCoroutine(LoadSceneCoroutine());
        }

        private IEnumerator LoadSceneCoroutine() {
            var loading = SceneManager.LoadSceneAsync(_loadingSceneName);
            while (!loading.isDone) {
                yield return null;
            }
            PrepareScene();
            _loadingSceneName = null;
            _fader.FadeIn();
        }

        private void PrepareScene() {
            string currentSceneName = SceneManager.GetActiveScene().name;
            if (currentSceneName == _menuSceneName) {
                HideAllScreens();
                ShowMenuScreen();
                _musicManager.PlayMenuMusic();
            } else if (currentSceneName == _gameplaySceneName) {
                HideAllScreens();
                ShowGameScreen();
                _musicManager.StopMusic();
            } else {
                HideAllScreens();
                _musicManager.StopMusic();
            }
        }


        // Screen showing/hiding methods.

        public void ShowMenuScreen() {
            _menuScreen.SetActive(true);
        }

        public void ShowGameScreen() {
            _gameScreen.SetActive(true);
        }

        public void ShowLeaderboardScreen() {
            _leaderboardScreen.SetActive(true);
        }

        public void HideAllScreens() {
            _menuScreen.SetActive(false);
            _gameScreen.SetActive(false);
            _leaderboardScreen.SetActive(false);
        }
        

    }

}
