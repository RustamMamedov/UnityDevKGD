﻿using System.Collections;
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
            _fader.OnFadeIn += OnSceneFadeIn;
            _fader.FadeIn();
        }

        private void Update() {
            Debug.Log("Update");
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

        private void ShowMenuScreen() {
            _menuScreen.SetActive(true);
        }

        private void ShowGameScreen() {
            _gameScreen.SetActive(true);
        }

        private void ShowLeaderboardScreen() {
            _leaderboardScreen.SetActive(true);
        }

        private void HideAllScreens() {
            _menuScreen.SetActive(false);
            _gameScreen.SetActive(false);
            _leaderboardScreen.SetActive(false);
        }

    }
}