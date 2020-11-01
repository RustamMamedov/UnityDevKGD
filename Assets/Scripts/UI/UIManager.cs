using System;
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
        private GameObject _leaderBoardScreen;

        private void Awake() {
            if (Instance) {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start() {
            //_fader.OnFadeIn += OnSceneFadeIn;
            //_fader.FedeIn();
        }

        public void LoadMenu() {
            
        }

        public void LoadGamePlay() {
            _fader.OnFadeOut += LoadGamePlay;
            _fader.FedeOut();
        }

        private void OnSceneFadeIn() {
            StartCoroutine(FadeOutAndLoadGameplay());
        }

        private IEnumerator FadeOutAndLoadGameplay() {
            yield return new WaitForSeconds(1f);

            _fader.OnFadeOut += LoadGameplayScene;
            _fader.FedeOut();
        }

        private void LoadGameplayScene() {
            _fader.OnFadeOut-=LoadGameplayScene;
            //StartCoroutine(LoadGameplaySceneCoroutine(_сurrentSceneName));
            //_сurrentSceneName = "Gameplay";// _CurrentSceneName == "Gameplay" ? "Menu" : "Gameplay";
        }

        private IEnumerator LoadGameplaySceneCoroutine(string SceneName) {
            var asyncOperation = SceneManager.LoadSceneAsync(SceneName);
            while (!asyncOperation.isDone) {
                yield return null;
            }
            yield return new WaitForSeconds(1f);

            _fader.FedeIn();
        }
        public void ShowMenuScreen() {
            _menuScreen.SetActive(true);
        }

        public void ShowGameScreen() {
            _gameScreen.SetActive(true);
        }

        public void ShowLeaderboardsScreen() {
            _leaderBoardScreen.SetActive(true);
        }

        public void HideAllScreens() {
            _menuScreen.SetActive(false);
            _gameScreen.SetActive(false);
            _leaderBoardScreen.SetActive(false);
        }
    }
}