using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Events;
using Audio;

namespace UI {

    public class UIManager : MonoBehaviour {
        [SerializeField]
        private GameObject _menuScreen;

        [SerializeField]
        private GameObject _gameScreen;

        [SerializeField]
        private GameObject _leaderboardsScreen;

        [SerializeField]
        private GameObject _optionScreen;


        public static UIManager Instance;

        [SerializeField]
        private Fader _fader;

        [SerializeField]
        private EventListener _saveData;

        [SerializeField]
        private MusicManager _musicManager;

        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            _saveData.OnEventHappened += ShowLeaderboardsScreen;
        }

        private void Start() {
            ShowMenuScreen();
        }

        public void LoadMenu() {
            _fader.OnFadeOut += LoadMenuScene;
            _fader.FadeOut();
        }


        private void LoadMenuScene() {
            _fader.OnFadeOut -= LoadMenuScene;
            StartCoroutine(LoadSceneCoroutine("Menu"));
            _musicManager.PlayMenuMusic();
            ShowMenuScreen();
        }

        public void LoadGameplay() {
            _fader.OnFadeOut += LoadGameplayScene;
            _fader.FadeOut();
        }

        private void LoadGameplayScene() {
            _fader.OnFadeOut -= LoadGameplayScene;
            StartCoroutine(LoadSceneCoroutine("Game"));
            _musicManager.PlayGameMusic();
            ShowGameScreen();
        }

        private IEnumerator LoadSceneCoroutine(string sceneName) {
            var asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            while (!asyncOperation.isDone) {
                yield return null;
            }
            _fader.FadeIn();
        }

#region Show\Hide

        public void ShowMenuScreen() {
            HideAllScreens();
            _menuScreen.SetActive(true);
            _musicManager.PlayMenuMusic();
        }

        public void ShowGameScreen() {
            HideAllScreens();
            _gameScreen.SetActive(true);
        }

        public void ShowLeaderboardsScreen() {
            HideAllScreens();
            _leaderboardsScreen.SetActive(true);
        }

        public void ShowOptionScreen() {
            HideAllScreens();
            _optionScreen.SetActive(true);
        }

        public void HideAllScreens() {
            _menuScreen.SetActive(false);
            _gameScreen.SetActive(false);
            _leaderboardsScreen.SetActive(false);
            _optionScreen.SetActive(false);
        }

#endregion Show\Hide
    }
}