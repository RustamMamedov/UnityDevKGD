using Events;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Audio;

namespace UI {

    public class UIManager : MonoBehaviour {

        public static UIManager Instance;

        [SerializeField]
        private Fader _fader;

        [SerializeField]
        private GameObject _gameScreen;

        [SerializeField]
        private GameObject _menuScreen;

        [SerializeField]
        private GameObject _leaderboardScreen;

        [SerializeField]
        private EventListener _endingSave;

        [SerializeField]
        private MusicManager _musicManager;

        //private string _currentSceneName = "Gameplay";

        public void ShowMenuScreen(){
            HideAllScreens();
            _menuScreen.SetActive(true);
            _musicManager.PlayMenuMusic();
        }

        public void ShowGameScreen(){
            HideAllScreens();
            _gameScreen.SetActive(true);
        }

        public void ShowLeaderboardScreen(){
            HideAllScreens();
            _leaderboardScreen.SetActive(true);
        }

        public void HideAllScreens(){
            _gameScreen.SetActive(false);
            _menuScreen.SetActive(false);
            _leaderboardScreen.SetActive(false);
        }

        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }
            _endingSave.OnEventHappened += ShowLeaderboardScreen;
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        //private void OnSceneFadeIn() {
        //    StartCoroutine(FadeOutAndLoadGameplay());
        //}

        private void Start() {
            ShowMenuScreen();
        }

        public void LoadMenu() {
            _fader.OnFadeOut += LoadMenuScene;
            _fader.FadeOut();

        }

        public void LoadGameplay() {
            _fader.OnFadeOut += LoadGameplayScene;
            _fader.FadeOut();
        }

        private void LoadMenuScene() {
            _fader.OnFadeOut -= LoadMenuScene;
            StartCoroutine(LoadSceneCoroutine("Menu"));
            //_currentSceneName = _currentSceneName == "Gameplay" ? "Menu" : "Gameplay";
            ShowMenuScreen();
        }

        private void LoadGameplayScene() {
            _fader.OnFadeOut -= LoadGameplayScene;
            StartCoroutine(LoadSceneCoroutine("Gameplay"));
            ShowGameScreen();
        }


        /*private IEnumerator FadeOutAndLoadGameplay() {
            yield return new WaitForSeconds(3f);

            _fader.OnFadeOut += LoadGameplayScene;
            _fader.FadeOut();
        }*/


        private IEnumerator LoadSceneCoroutine(string sceneName) {
            var asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            while (!asyncOperation.isDone) {
                yield return null;
            }
            yield return new WaitForSeconds(3f);
            _fader.FadeIn();
        }

        //public void ShowMenuScreen() {
        //    _menuScreen.SetActive(true);
        //}
    }
}