using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Game;
using Audio;
using Events;

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

        [SerializeField]
        private Text _currentScoreText;

        [SerializeField]
        private ScriptableIntValue _currentScore;

        [SerializeField]
        private MusicManager _musicManager;

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

        public void LoadLeaderboardScreen() {
            StartCoroutine(LoadSceneCoroutine("Menu"));
            ShowLeaderboardsScreen();
        }

        public void LoadGameplay() {
            _fader.OnFadeOut += LoadGameplayScene;
            _fader.FadeOut();
        }

        private void LoadMenuScene() {
            _currentScoreText.text ="0";
            _currentScore.value = 0;
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
            if (_menuScreen.activeSelf==false) {
            _menuScreen.SetActive(true);
                _musicManager.PlayMenuMusic();
            }
        }

         public void ShowGameScreen() {
             HideAllScreens();
             if (_gameScreen.activeSelf==false) {
                 _gameScreen.SetActive(true);
             }
         }

         public void ShowLeaderboardsScreen() {
            HideAllScreens();
            if (_leaderboardScreen.activeSelf==false) {
                 _leaderboardScreen.SetActive(true);
            }
         }

         public void HideAllScreens() {

                    if (_menuScreen.activeSelf==true) {
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
