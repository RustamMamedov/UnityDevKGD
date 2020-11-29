using System;
using System.Collections;
using Audio;
using Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI {

    public class UIManager : MonoBehaviour {

        public static UIManager Instance { get; private set; }

        [SerializeField] 
        private Fader _fader;

        [Header("UI Screens")]
        [SerializeField]
        private GameObject _menuScreen;

        [SerializeField]
        private GameObject _gameScreen;

        [SerializeField]
        private GameObject _leaderboardScreen;
        
        [SerializeField] 
        private GameObject _settingsScreen;

        [SerializeField] 
        private EventListener _dataSavedListener;

        [SerializeField] 
        private MusicManager _musicManager;
        
        private void Start() {
            HideAllScreens();
            ShowMenuScreen();
        }

        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            _dataSavedListener.OnEventHappened += ShowLeaderboardScreen;
        }

        private void OnDestroy() {
            _dataSavedListener.OnEventHappened -= ShowLeaderboardScreen;
        }

        public void LoadMenu() {
            _fader.OnFadeOut += LoadMenuScene;
            _fader.FadeOut();
        }

        public void LoadGameplay() {
            _fader.OnFadeOut += LoadGameplayScene;
            _fader.FadeOut();
        }
        
        private void LoadGameplayScene() {
            _fader.OnFadeOut -= LoadGameplayScene;
            StartCoroutine(LoadSceneCoroutine("Gameplay"));
            ShowGameScreen();
        }
        
        private void LoadMenuScene() {
            _fader.OnFadeOut -= LoadMenuScene;
            StartCoroutine(LoadSceneCoroutine("Menu"));
            ShowMenuScreen();
        }

        private IEnumerator FadeOutAndLoadGameplay() {
            yield return new WaitForSeconds(3f);

            _fader.OnFadeOut += LoadGameplayScene;
            _fader.FadeOut();
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
            _gameScreen.SetActive(true);
            _musicManager.PlayGameMusic(); 
        }

        public void ShowLeaderboardScreen() {
            HideAllScreens();
            _leaderboardScreen.SetActive(true);
        }

        public void ShowSettingsScreen() {
            HideAllScreens();
            _settingsScreen.SetActive(true);
        }

        public void HideAllScreens() {
            _menuScreen.SetActive(false);
            _gameScreen.SetActive(false);
            _leaderboardScreen.SetActive(false);
            _settingsScreen.SetActive(false);
        }
      
    }
}
