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


        // Scene loading.

        public void LoadGameplayScene() {
            _fader.OnFadeOut += PrepareGameplayScene;
            _fader.FadeOut();
        }

        public void LoadMenuScene() {
            _fader.OnFadeOut += PrepareMenuScene;
            _fader.FadeOut();
        }

        private void PrepareGameplayScene() {
            _fader.OnFadeOut -= PrepareGameplayScene;
            HideAllScreens();
            ShowGameScreen();
            StartCoroutine(LoadSceneCoroutine(_gameplaySceneName));
        }

        private void PrepareMenuScene() {
            _fader.OnFadeOut -= PrepareMenuScene;
            HideAllScreens();
            ShowMenuScreen();
            StartCoroutine(LoadSceneCoroutine(_menuSceneName));
        }

        private IEnumerator LoadSceneCoroutine(string name) {
            var loading = SceneManager.LoadSceneAsync(name);
            while (!loading.isDone) {
                yield return null;
            }
            _fader.FadeIn();
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
