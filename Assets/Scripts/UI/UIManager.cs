﻿using System.Collections;
using Events;
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
        private GameObject _leaderBoardScreen;

        [SerializeField]
        private EventListeners _saveDataEventListeners;

        [SerializeField]
        private MusicManager _musicManager;

        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }
            _saveDataEventListeners.OnEventHappened += ShowLeaderboardsScreen;

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start() {
            _musicManager.PlayMenuMusic();
            ShowMenuScreen();
        }
        public void LoadMenu() {
            _musicManager.StopGameMusic();
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
            ShowMenuScreen();
            _musicManager.PlayMenuMusic();
        }
        private void LoadGameplayScene() {
            _fader.OnFadeOut -= LoadGameplayScene;
            StartCoroutine(LoadSceneCoroutine("Gameplay"));
            ShowGameScreen();
            _musicManager.PlayGameMusic();
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
            //_musicManager.PlayMenuMusic();
        }

        public void ShowGameScreen() {
            HideAllScreens();
            _gameScreen.SetActive(true);
        }

        public void ShowLeaderboardsScreen() {
            HideAllScreens();
            _leaderBoardScreen.SetActive(true);
        }

        public void HideAllScreens() {
            _menuScreen.SetActive(false);
            _gameScreen.SetActive(false);
            _leaderBoardScreen.SetActive(false);
        }
    }
}